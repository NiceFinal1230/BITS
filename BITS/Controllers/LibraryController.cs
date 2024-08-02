using BITS.Areas.Identity.Data;
using BITS.Data;
using BITS.Models;
using BITS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BITS.Controllers
{
    public class LibraryController : Controller
    {

        private readonly BITSContext _context;
        private UserManager<BITSUser> _userManager;

        public LibraryController(BITSContext context, UserManager<BITSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(string sortOrder)
        {
            List<string> options = new List<string>()
            {
                "Alphabetical",
                "Oldest",
                "Recent"
            };

            ViewBag.Options = new SelectList(options);

            List<string> views = new List<string>()
            {
                "Favorite",
                "All"
            };

            ViewBag.View = new SelectList(options);

            ViewBag.SelectedOption = string.IsNullOrEmpty(sortOrder) ? "Sort By" : sortOrder;

            var user = await _userManager.GetUserAsync(User);
            
            var user_orders = await _context.Order.Where(i => i.UserId == user!.Id).ToListAsync();

            List<UserOrdersViewModel> vm = new();
            foreach (var order in user_orders)
            {
                var products_in_orders = await _context.ProductsInOrders.Where(i => i.OrderId == order.OrderId).ToListAsync();
                foreach (var i in products_in_orders)
                {
                    var p = await _context.Product.FindAsync(i.Products);
                    if (p != null)
                    {
                        UserOrdersViewModel temp = new() { Product = p, Quantity = i.Quantity, ProductsInOrders = i, Order = order };
                        vm.Add(temp);
                    }
                }

            }

            vm = sortOrder switch
            {
                "Alphabetical" => vm.OrderBy(v => v.Product.Name).ToList(),
                "Oldest" => vm.OrderBy(v => v.Order.DateOfPurchase).ToList(),
                "Recent" => vm.OrderByDescending(v => v.Order.DateOfPurchase).ToList(), _ => vm
            };

            ViewBag.Library = vm;

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            try
            {
                var order = await _context.Order.FindAsync(id);
                if (order != null)
                {
                    order.Favorite = !order.Favorite;
                    _context.Order.Update(order);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
