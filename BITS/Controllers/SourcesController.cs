using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BITS.Data;
using BITS.Models;
using BITS.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace BITS.Controllers
{
    public class SourcesController : Controller
    {
        private readonly BITSContext _context;
        private UserManager<BITSUser> _userManager;

        public SourcesController(BITSContext context)
        {
            _context = context;
        }

        // GET: Sources
        public async Task<IActionResult> Index(string?[] types, string search)
        {
            ViewBag.Types = await _context.Source.Select(i => i.Types).Distinct().ToListAsync();

            // Search By Name
            if (search != null)
            {
                return View(await _context.Source.Where(i => i.Name.Contains(search)).ToListAsync());
            }

            // Search By Genres
            if (types != null && types.Length > 0)
            {
                HashSet<string> set = types.ToHashSet();
                return View(await _context.Source.Where(i => set.Contains(i.Types)).ToListAsync());
            }

            return View(await _context.Source.ToListAsync());
        }

        // GET: Sources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await _context.Source
                .FirstOrDefaultAsync(m => m.Id == id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // GET: Sources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Source source)
        {
            if (ModelState.IsValid)
            {
                _context.Add(source);
                await _context.SaveChangesAsync();
                return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
            }
            return View(source);
        }

        // GET: Sources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var source = await _context.Source.FindAsync(id);
            if (source == null)
            {
                return NotFound();
            }
            return View(source);
        }

        // POST: Sources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Source source)
        {
            if (id != source.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(source);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SourceExists(source.Id))
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
            return View(source);
        }

        // GET: Sources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var source = await _context.Source.FindAsync(id);
            if (source != null)
            {
                _context.Source.Remove(source);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(controllerName: "Products", actionName: "CloseIFrame");
        }

        public async Task<IActionResult> DeleteFromIndex(int id)
        {
            var source = await _context.Source.FindAsync(id);
            if (source != null)
            {
                _context.Source.Remove(source);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SourceExists(int id)
        {
            return _context.Source.Any(e => e.Id == id);
        }
    }
}
