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
            try
            {
                var user = await _userManager.GetUserAsync(User);
                
                List<Cart> carts = (from _cart in _context.Cart
                                    where _cart.UserId == user.Id
                                    select _cart).ToList();
                if (carts == null)
                    carts = new List<Cart>();
                else
                {
                    List<CartViewModel> list = new List<CartViewModel>();

                    ViewBag.UserId = user.Id;
                    foreach(Cart i in carts)
                    {
                        var item = await _context.Product.FindAsync(i.ProductId);
                        if(item != null)
                        {
                            var cartViewModel = new CartViewModel
                            {
                                ProductId = i.ProductId,
                                Name = item.Name,
                                ProductType = "Base Game",
                                Price = "99",
                                Picture = "",
                                RefundType = "Self-Refundable",
                                Quantity = i.Quantity
                            };
                            list.Add(cartViewModel);
                        }

                    }
                    return View(list);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return NotFound();
        }
        [Authorize]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
                return NotFound();

            int productId = (int)id;
            try
            {
                var prod = await _context.Product.FindAsync(productId);
                var user = await _userManager.GetUserAsync(User);
                var item = new Cart(user.Id, productId, 1);
                var cart = (from _cart in _context.Cart
                           where _cart.UserId == user.Id && _cart.ProductId == productId
                           select _cart).FirstOrDefault();
                if (cart == null)
                    _context.Cart.Add(item);
                else
                {
                    cart.Quantity = cart.Quantity + 1;
                    _context.Cart.Update(cart);
                }
                await _context.SaveChangesAsync();
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            return RedirectToAction(controllerName: "Products", actionName: "Details", routeValues: new { id = productId });
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            int productId = id;
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var cart = await _context.Cart.FirstOrDefaultAsync(c => c.UserId == user.Id && c.ProductId == productId);

                if (cart != null)
                {
                    _context.Cart.Remove(cart);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
