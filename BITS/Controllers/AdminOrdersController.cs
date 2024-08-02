using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BITS.Data;
using BITS.Models;
using BITS.ViewModel;
using Azure.Identity;
using BITS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace BITS.Controllers
{
    public class AdminOrdersController : Controller
    {
        private readonly BITSContext _context;
        private UserManager<BITSUser> _userManager;

        public AdminOrdersController(BITSContext context, UserManager<BITSUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AdminOrders
        public async Task<IActionResult> Index(string? start_date, string? end_date, string search)
        {
            // Search By Id
            if (search != null)
            {
                return View(await _context.Order.Where(i => i.OrderId.ToString().Contains(search)).ToListAsync());
            }

            // Search By Date
            if (start_date != null && end_date != null)
            {
                var parsed_start_date = DateTime.Parse(start_date);
                var parsed_end_date = DateTime.Parse(end_date);

                return View(await _context.Order.Where(i => i.DateOfPurchase >= parsed_start_date && i.DateOfPurchase <= parsed_end_date).ToListAsync());
            }

            return View(await _context.Order.ToListAsync());
        }

        // GET: AdminOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            var raw = await _context.ProductsInOrders.Where(i => i.OrderId == id).ToListAsync();
            List<ProductStocktakeViewModel> vm = new();
            foreach (var i in raw)
            {
                var p = await _context.Product.FindAsync(i.Products);
                if (p != null)
                {
                    ProductStocktakeViewModel temp = new ProductStocktakeViewModel { Product = p, Quantity = i.Quantity, ProductsInOrders=i };
                    vm.Add(temp);
                }
            }
            ViewBag.ProductList = vm;
            return View(order);
        }

        // GET: AdminOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OriginalPrice,Subtotal,DiscountPrice,UserId,StreetAddress,PostCode,Suburb,State")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
            }
            return View(order);
        }

        // GET: AdminOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.ProductList = await PopulateProductList(id);
            return View(order);
        }

        private async Task<List<ProductStocktakeViewModel>> PopulateProductList(int? id)
        {
            var raw = await _context.ProductsInOrders.Where(i => i.OrderId == id).ToListAsync();
            List<ProductStocktakeViewModel> vm = new();
            foreach (var i in raw)
            {
                var p = await _context.Product.FindAsync(i.Products);
                if (p != null)
                {
                    ProductStocktakeViewModel temp = new ProductStocktakeViewModel { Product = p, Quantity = i.Quantity, ProductsInOrders = i };
                    vm.Add(temp);
                }
            }
            return vm;
        }

        // POST: AdminOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
            }
            ViewBag.ProductList = await PopulateProductList(id);
            return View(order);
        }

        // GET: AdminOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteFromIndex(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
