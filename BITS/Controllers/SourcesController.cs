using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BITS.Data;
using BITS.Models;
using Microsoft.AspNetCore.Authorization;

namespace BITS.Controllers;

[Authorize(Roles = "Administrator")]
public class SourcesController : Controller
{
    private readonly BITSContext _context;

    public SourcesController(BITSContext context)
    {
        _context = context;
    }

    // GET: Sources
    public async Task<IActionResult> Index()
    {
        return View(await _context.Source.ToListAsync());
    }

    // GET: Sources/Details/5
    public async Task<IActionResult> Details(string id)
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
    public async Task<IActionResult> Create([Bind("Id,Name,ExternalLink")] Source source)
    {
        if (ModelState.IsValid)
        {
            _context.Add(source);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(source);
    }

    // GET: Sources/Edit/5
    public async Task<IActionResult> Edit(string id)
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
    public async Task<IActionResult> Edit(string id, [Bind("Id,Name,ExternalLink")] Source source)
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
            return RedirectToAction(nameof(Index));
        }
        return View(source);
    }

    // GET: Sources/Delete/5
    public async Task<IActionResult> Delete(string id)
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

    // POST: Sources/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var source = await _context.Source.FindAsync(id);
        if (source != null)
        {
            _context.Source.Remove(source);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SourceExists(string id)
    {
        return _context.Source.Any(e => e.Id == id);
    }
}
