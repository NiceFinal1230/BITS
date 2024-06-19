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
using Microsoft.CodeAnalysis;

namespace BITS.Controllers
{
    public class StocktakesController : Controller
    {
        private readonly BITSContext _context;

        public StocktakesController(BITSContext context)
        {
            _context = context;
        }

        // GET: Stocktakes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stocktake.ToListAsync());
        }

        // GET: Stocktakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocktake = await _context.Stocktake
                .FirstOrDefaultAsync(m => m.StocktakeId == id);
            if (stocktake == null)
            {
                return NotFound();
            }

            return View(stocktake);
        }

        // GET: Stocktakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stocktakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Stocktake stocktake, int SourceId)
        {

            var isProductExist = await _context.Product.FindAsync(stocktake.ProductId);
            if(isProductExist == null)
                return NotFound();

            var isSourceExist = await _context.Source.FindAsync(SourceId);
            if (isSourceExist == null)
                return NotFound();
            stocktake.SourceId = SourceId;

            if (ModelState.IsValid)
            {
                _context.Add(stocktake);
                await _context.SaveChangesAsync();
                return RedirectToAction(controllerName: "Products", actionName: "Edit", routeValues: new { id = stocktake.ProductId });
            }
            return View(stocktake);
        }

        // GET: Stocktakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocktake = await _context.Stocktake.FindAsync(id);
            if (stocktake == null)
            {
                return NotFound();
            }

            return View(stocktake);
        }

        // POST: Stocktakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Stocktake stocktake)
        {
            if (id != stocktake.StocktakeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stocktake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StocktakeExists(stocktake.StocktakeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(stocktake);
        }

        // GET: Stocktakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocktake = await _context.Stocktake
                .FirstOrDefaultAsync(m => m.StocktakeId == id);
            if (stocktake == null)
            {
                return NotFound();
            }

            return View(stocktake);
        }

        // POST: Stocktakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stocktake = await _context.Stocktake.FindAsync(id);
            if (stocktake != null)
            {
                _context.Stocktake.Remove(stocktake);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StocktakeExists(int id)
        {
            return _context.Stocktake.Any(e => e.StocktakeId == id);
        }
    }
}
