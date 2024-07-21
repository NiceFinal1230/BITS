using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BITS.Data;
using BITS.Models;

namespace BITS.Controllers
{
    public class ProductsInOrdersController : Controller
    {
        private readonly BITSContext _context;

        public ProductsInOrdersController(BITSContext context)
        {
            _context = context;
        }

        // GET: ProductsInOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductsInOrders.ToListAsync());
        }

        // GET: ProductsInOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsInOrders = await _context.ProductsInOrders
                .FirstOrDefaultAsync(m => m.ProductsInOrdersId == id);
            if (productsInOrders == null)
            {
                return NotFound();
            }

            return View(productsInOrders);
        }

        // GET: ProductsInOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsInOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductsInOrdersId,OrderId,Products,Quantity")] ProductsInOrders productsInOrders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productsInOrders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productsInOrders);
        }

        // GET: ProductsInOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsInOrders = await _context.ProductsInOrders.FindAsync(id);
            if (productsInOrders == null)
            {
                return NotFound();
            }
            return View(productsInOrders);
        }

        // POST: ProductsInOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductsInOrdersId,OrderId,Products,Quantity")] ProductsInOrders productsInOrders)
        {
            if (id != productsInOrders.ProductsInOrdersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productsInOrders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsInOrdersExists(productsInOrders.ProductsInOrdersId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(controllerName: "AdminOrders", actionName: "Edit", routeValues: new { id = productsInOrders.OrderId });
            }
            return View(productsInOrders);
        }

        // POST: ProductsInOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productsInOrders = await _context.ProductsInOrders.FindAsync(id);
            if (productsInOrders != null)
            {
                _context.ProductsInOrders.Remove(productsInOrders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(controllerName: "AdminOrders", actionName: "Edit", routeValues: new { id = id });
        }

        private bool ProductsInOrdersExists(int id)
        {
            return _context.ProductsInOrders.Any(e => e.ProductsInOrdersId == id);
        }
    }
}
