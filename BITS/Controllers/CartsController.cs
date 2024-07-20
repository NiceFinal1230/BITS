using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BITS.Data;
using BITS.Models;
using Microsoft.AspNetCore.Identity;
using BITS.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using BITS.ViewModel;
using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BITS.Services;

namespace BITS.Controllers
{
    public class CartsController : Controller
    {
        private readonly BITSContext _context;
        private UserManager<BITSUser> _userManager;

        public CartsController(BITSContext context, UserManager<BITSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Carts
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return NotFound();
            }

            ViewBag.UserId = user.Id;

            List<Cart> carts = (from _cart in _context.Cart
                                where _cart.UserId == user.Id
                                select _cart).ToList();

            if (carts == null)
                carts = new();

            CartViewModel vm = new();

            foreach(Cart i in carts)
            {
                var item = await _context.Product.FindAsync(i.ProductId);
                if(item != null)
                {
                    var stocktake = await _context.Stocktake.FirstOrDefaultAsync(x => x.ProductId == i.ProductId && x.Quantity >= i.Quantity);
                    if (stocktake == null)
                    {
                        vm.RunOut.Add(i.ProductId);
                        vm.ProductStocktake.Add(new ProductStocktakeViewModel { Product = item, Stocktake = new(), Quantity = i.Quantity });
                    }
                    else
                    {
                        vm.ProductStocktake.Add(new ProductStocktakeViewModel { Product = item, Stocktake = stocktake, Quantity = i.Quantity });
                    }
                }
            }

            double prices = 0;
            double discount = 0;
            double discountedPrice = 0;
            double subtotal = 0;

            foreach(var i in vm.ProductStocktake)
            {
                double originalPrice = i.Product.Price * i.Quantity;
                double discountRate = i.Stocktake.DiscountRate / 100;
                double currentDiscount = originalPrice * discountRate;
                prices += originalPrice; // Total original prices
                discount += currentDiscount; // Total discounts
                discountedPrice = originalPrice - currentDiscount; // Price after individual discount
                subtotal += discountedPrice; // Total after all individual discounts
            }

            vm.Prices = Math.Round(prices, 2);
            vm.Discount = Math.Round(discount, 2);
            /*vm.Subtotal = Math.Round(prices - discount, 2);*/
            vm.Subtotal = Math.Round(subtotal, 2);
            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var prod = await _context.Product.FindAsync(id);
                var user = await _userManager.GetUserAsync(User);
                var item = new Cart { UserId = user!.Id, ProductId = (int)id, Quantity = 1 };
                var cart = (from _cart in _context.Cart
                           where _cart.UserId == user.Id && _cart.ProductId == id
                            select _cart).FirstOrDefault();

                if (cart == null)
                    _context.Cart.Add(item);
                else
                {
                    cart.Quantity++;
                    _context.Cart.Update(cart);
                }
                await _context.SaveChangesAsync();
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            return RedirectToAction(controllerName: "Products", actionName: "Details", routeValues: new { id = id });
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            int productId = id;
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var cart = await _context.Cart.FirstOrDefaultAsync(c => c.UserId == user.Id && c.ProductId == productId);
                if(cart == null)
                {
                    return NotFound();
                }
                cart.Quantity--;
                if (cart.Quantity <= 0)
                {
                    _context.Cart.Remove(cart);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(string? products)
        {
            if(products != null)
            {
                var cvm = Newtonsoft.Json.JsonConvert.DeserializeObject<CartViewModel>(products);
                if(cvm != null)
                {
                    HttpContext.Session.Set<CartViewModel>("products", cvm);
                    HttpContext.Session.SetInt32("", 73);
                }
            }
            HttpContext.Session.Remove("product");
            return RedirectToAction(controllerName: "Orders", actionName: "Index");
        }
        
    }
}
