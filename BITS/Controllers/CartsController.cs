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
            // Check if the product ID is null
            if (id == null)
                return NotFound();

            try
            {
                // Find the product in the database using the provided ID
                var prod = await _context.Product.FindAsync(id);
                // Get the currently logged-in user
                var user = await _userManager.GetUserAsync(User);
                // Create a new Cart item with the user's ID and the product ID, initializing the quantity to 1
                var item = new Cart { UserId = user!.Id, ProductId = (int)id, Quantity = 1 };
                // Check if the cart already contains this product for the current user
                var cart = (from _cart in _context.Cart
                           where _cart.UserId == user.Id && _cart.ProductId == id
                            select _cart).FirstOrDefault();
                // If the product is not in the cart, add the new item
                // If the product is already in the cart, increase the quantity
                if (cart == null)
                    _context.Cart.Add(item);
                else
                {
                    cart.Quantity++;
                    _context.Cart.Update(cart);
                }
                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            return RedirectToAction(controllerName: "Products", actionName: "Details", routeValues: new { id = id });
        }


        //Get: Remove item from cart
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            int productId = id;
            try
            {
                // Get the currently logged-in user
                var user = await _userManager.GetUserAsync(User);

                // Find the cart item for the current user and the specified product
                var cart = await _context.Cart.FirstOrDefaultAsync(c => c.UserId == user.Id && c.ProductId == productId);

                // If the cart item is not found, return a NotFound result
                if(cart == null)
                {
                    return NotFound();
                }

                // Decrement the quantity of the product in the cart
                cart.Quantity--;

                // If the quantity is less than or equal to zero, remove the cart item
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
            // Check if the products parameter is not null
            if(products != null)
            {
                // Deserialize the products JSON string into a CartViewModel object
                var cvm = Newtonsoft.Json.JsonConvert.DeserializeObject<CartViewModel>(products);

                // If deserialization is successful and cvm is not null
                if(cvm != null)
                {
                    // Store the CartViewModel object in the session with the key "products"
                    HttpContext.Session.Set<CartViewModel>("products", cvm);
                    // Store an integer value in the session with an empty key (this is likely a mistake or placeholder)
                    HttpContext.Session.SetInt32("", 73);
                }
            }
            // Remove any existing "product" key from the session
            HttpContext.Session.Remove("product");
            return RedirectToAction(controllerName: "Orders", actionName: "Index");
        }
        
    }
}
