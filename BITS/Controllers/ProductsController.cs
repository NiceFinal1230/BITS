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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BITS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BITSContext _context;

        public ProductsController(BITSContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Developer,Publisher,Description,Genres,Features,ReleaseDate,LastUpdated")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            PopulateAssignedGenresData(product);
            PopulateAssignedFeaturesData(product);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, string[] selectedGenres, string[] selectedFeatures)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if(product.Genres == null)
                product.Genres = new List<Genre>();
            if (selectedGenres != null)
            {
                var list = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
                var set = new HashSet<string>(selectedGenres);
                foreach(var i in list)
                {
                    if (set.Contains(i.ToString()))
                    {
                        if (!product.Genres.Contains(i))
                            product.Genres.Add(i);
                    }
                    else
                    {
                        if (product.Genres.Contains(i))
                            product.Genres.Remove(i);
                    }
                }
            }

            if(product.Features == null)
                product.Features = new List<Features>();
            if (selectedFeatures != null)
            {
                var list = Enum.GetValues(typeof(Features)).Cast<Features>().ToList();
                var set = new HashSet<string>(selectedFeatures);
                foreach (var i in list)
                {
                    if (set.Contains(i.ToString()))
                    {
                        if (!product.Features.Contains(i))
                            product.Features.Add(i);
                    }
                    else
                    {
                        if (product.Features.Contains(i))
                            product.Features.Remove(i);
                    }
                }
            }

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            PopulateAssignedFeaturesData(product);
            PopulateAssignedGenresData(product);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }

        private void PopulateAssignedGenresData(Product product)
        {
            var list = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
            var set = new HashSet<Genre>(product.Genres);
            var viewModel = new List<AssignedGenresData>();
            foreach (var i in list)
            {
                viewModel.Add(new AssignedGenresData
                {
                    Name = i,
                    Assigned = set.Contains(i)
                });
            }
            ViewData["Genres"] = viewModel;
        }
        private void PopulateAssignedFeaturesData(Product product)
        {
            var list = Enum.GetValues(typeof(Features)).Cast<Features>().ToList();
            var set = new HashSet<Features>(product.Features);
            var viewModel = new List<AssignedFeaturesData>();
            foreach (var i in list)
            {
                viewModel.Add(new AssignedFeaturesData
                {
                    Name = i,
                    Assigned = set.Contains(i)
                });
            }
            ViewData["Features"] = viewModel;
        }
    }
}
