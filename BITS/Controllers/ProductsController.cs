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
using System.Net;
using Azure.Core;

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
        public async Task<IActionResult> Index(string[]? genres, string? search)
        {
            // Search By Name
            if (search != null)
            {
                var temp = await _context.Product.Where(product => product.Name.Contains(search)).ToListAsync();
                return View(temp);
            }

            // Search By Genres
            if (genres != null && genres.Length > 0)
            {
                List<Product> list = await _context.Product.ToListAsync();
                List<Product> result = new();
                HashSet<string> set = genres.ToHashSet();
                foreach(var product in list)
                {
                    var productGenres = product.Genres.ToHashSet();
                    foreach(var i in productGenres)
                    {
                        if (set.Contains(i.ToString()))
                        {
                            result.Add(product);
                            break;
                        }
                    }
                }
                return View(result);
            }

            // Default View
            return View(await _context.Product.ToListAsync());
        }

        public async Task<IActionResult> AddOn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await (from _addon in _context.Product
                          where _addon.BaseGameId == id
                          select _addon).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            List<ProductStocktakeViewModel> vm = new();
            foreach(var product in products)
            {
                var result = _context.Stocktake.FirstOrDefault(i => i.ProductId == product.ProductId);
                if(result != null)
                    vm.Add(new ProductStocktakeViewModel { Product = product, Stocktake = result });
                else
                {
                    vm.Add(new ProductStocktakeViewModel { Product = product, Stocktake = new() });
                }
            }

            var baseProduct = await _context.Product.FindAsync(id);

            if (baseProduct != null)
            {
                ViewBag.BaseProduct = baseProduct;
            }

            return View(vm);
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

            ViewBag.Stocktake = await _context.Stocktake
                .Where(stock => stock.ProductId == id)
                .ToListAsync();

            var stocks = await _context.Stocktake
                .Where(stock => stock.ProductId == id)
                .OrderByDescending(stock => stock.Price)
                .ToListAsync();

            if (stocks.Any())
            {
                var firstStock = stocks.First();
                ViewBag.Quantity = firstStock.Quantity;
                ViewBag.SotcktakeId = firstStock.StocktakeId;
                ViewBag.DiscountRate = firstStock.DiscountRate;
                ViewBag.DiscountPrice = product.Price * (100 - firstStock.DiscountRate) / 100;
                ViewBag.StartDate = firstStock.DiscountStartDate;
                ViewBag.EndDate = firstStock.DiscountEndDate;
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var product = new Product { Name = "Multiplayer Strategy", Developer = "Strategy Masters", Publisher = "Strategic Games", Description = "A strategy game with multiplayer features.", Genres = new List<Genre> { Genre.Strategy, Genre.Simulation }, PreviewImages = new List<string> { "https://plus.unsplash.com/premium_photo-1701214498603-59f754e55543?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxfHx8ZW58MHx8fHx8", "https://images.unsplash.com/photo-1718046254335-d9ff832c9c3c?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwyfHx8ZW58MHx8fHx8", "https://images.unsplash.com/photo-1718115257239-7246f434a7b0?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw0fHx8ZW58MHx8fHx8", "https://images.unsplash.com/photo-1689602037070-fec2eca3f5b2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw3fHx8ZW58MHx8fHx8", "https://images.unsplash.com/photo-1718019668172-5380fd58f2c5?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw4fHx8ZW58MHx8fHx8", "https://images.unsplash.com/photo-1718078742597-6d07da10820f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxMHx8fGVufDB8fHx8fA%3D%3D", "https://plus.unsplash.com/premium_photo-1709309934434-448b98065f66?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxN3x8fGVufDB8fHx8fA%3D%3D" }, Features = new List<Features> { Features.Multiplayer, Features.Coop }, ReleaseDate = new DateTime(2024, 2, 15), LastUpdated = DateTime.Now, OSMinimum = "Windows 7", OSRecommended = "Windows 10", ProcessorMinimum = "Intel Core i3", ProcessorRecommended = "Intel Core i5", MemoryMinimum = "4 GB RAM", MemoryRecommended = "8 GB RAM", StorageMinimum = "20 GB available space", StorageRecommended = "50 GB available space", GraphicsMinimum = "NVIDIA GeForce GTX 660", GraphicsRecommended = "NVIDIA GeForce GTX 1060", ProductType = "Base Game", RefundType = "Self-Refundable", Price = 99.99 };
            PopulateAssignedGenresData(product);
            PopulateAssignedFeaturesData(product);
            PopulatePreviewImages(product);
            return View(product);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Product product, string _previewImages, string[] selectedFeatures, string[] selectedGenres)
        //{
        //    SetGenres(product, selectedGenres);
        //    SetFeatures(product, selectedFeatures);
        //    SetPreviewImages(product, _previewImages);
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    PopulateAssignedGenresData(product);
        //    PopulateAssignedFeaturesData(product);
        //    Stocktake stocktake= new Stocktake { ProductId = product.ProductId };
        //    return View(new ProductStocktakeViewModel { Product = product, Stocktake = stocktake } );
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, string _previewImages, string[] selectedFeatures, string[] selectedGenres)
        {
            SetGenres(product, selectedGenres);
            SetFeatures(product, selectedFeatures);
            SetPreviewImages(product, _previewImages);
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CloseIFrame));
            }
            PopulateAssignedGenresData(product);
            PopulateAssignedFeaturesData(product);
            Stocktake stocktake = new Stocktake { ProductId = product.ProductId };
            return View(new ProductStocktakeViewModel { Product = product, Stocktake = stocktake });
        }
        public IActionResult CloseIFrame(int? id)
        {
            return View();
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

            ViewBag.sources = _context.Source.ToList();
            ViewBag.Context = _context;
            ViewBag.Stocktake = await _context.Stocktake.Where(stock => stock.ProductId == id).ToListAsync();

            PopulateAssignedGenresData(product);
            PopulateAssignedFeaturesData(product);
            PopulatePreviewImages(product);
            Stocktake stocktake = new Stocktake { ProductId = product.ProductId };
            return View(new ProductStocktakeViewModel { Product = product, Stocktake = stocktake });
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, string[] selectedGenres, string[] selectedFeatures, string _previewImages)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            SetPreviewImages(product, _previewImages);
            SetGenres(product, selectedGenres);
            SetFeatures(product, selectedFeatures);
            if (ModelState.IsValid)
            {
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
            }
            return RedirectToAction(nameof(CloseIFrame));
            //PopulateAssignedFeaturesData(product);
            //PopulateAssignedGenresData(product);
            //ViewBag.sources = _context.Source.ToList();
            //ViewBag.Context = _context;
            //ViewBag.Stocktake = await _context.Stocktake.Where(stock => stock.ProductId == id).ToListAsync();
            //Stocktake stocktake = new Stocktake { ProductId = product.ProductId };
            //return View(new ProductStocktakeViewModel { Product = product, Stocktake = stocktake });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(CloseIFrame));
        }

        public async Task<IActionResult> DeleteFromIndex(int id)
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

        private void PopulateAssignedGenresData(Product product = null)
        {
            
            var list = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
            var viewModel = new List<AssignedGenresData>();

            if (product != null && product.Genres != null)
            {
                var set = new HashSet<Genre>(product.Genres);
                foreach (var i in list)
                {
                    viewModel.Add(new AssignedGenresData { Name = i, Assigned = set.Contains(i) });
                }
            }
            else
            {
                foreach (var i in list)
                {
                    viewModel.Add(new AssignedGenresData { Name = i, Assigned = false });
                }
            }

            ViewData["Genres"] = viewModel;
        }
        private void PopulateAssignedFeaturesData(Product product = null)
        {
            var list = Enum.GetValues(typeof(Features)).Cast<Features>().ToList();

            var viewModel = new List<AssignedFeaturesData>();
            if (product != null && product.Features != null)
            {
                var set = new HashSet<Features>(product.Features);
                foreach (var i in list)
                {
                    viewModel.Add(new AssignedFeaturesData { Name = i, Assigned = set.Contains(i) });
                }
            }
            else
            {
                foreach (var i in list)
                {
                    viewModel.Add(new AssignedFeaturesData { Name = i, Assigned = false });
                }
            }

            ViewData["Features"] = viewModel;
        }

        private void PopulatePreviewImages(Product product = null)
        {
            var viewModel = "";
            if (product != null && product.PreviewImages.Count > 0)
            {
                foreach(var image in product.PreviewImages)
                {
                    viewModel = viewModel + image + System.Environment.NewLine;
                }
            }
            ViewData["PreviewImages"] = viewModel;
        }

        private void SetGenres(Product product, string[] selectedGenres)
        {
            if (product.Genres == null)
                product.Genres = new List<Genre>();
            if (selectedGenres != null)
            {
                var list = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
                var set = new HashSet<string>(selectedGenres);
                foreach (var i in list)
                {
                    if (set.Contains(i.ToString())) 
                        product.Genres.Add(i);
                    else
                        product.Genres.Remove(i);
                }
            }
        }
        private void SetFeatures(Product product, string[] selectedFeatures)
        {
            if (product.Features == null)
                product.Features = new List<Features>();
            if (selectedFeatures != null)
            {
                var list = Enum.GetValues(typeof(Features)).Cast<Features>().ToList();
                var set = new HashSet<string>(selectedFeatures);
                foreach (var i in list)
                {
                    if (set.Contains(i.ToString()))
                    {
                        product.Features.Add(i);
                    }
                    else
                    {
                        product.Features.Remove(i);
                    }
                }
            }
        }

        private void SetPreviewImages(Product product, string input)
        {
            if (input == null)
            {
                return;
            }

            // Split the input string by Environment.NewLine
            product.PreviewImages = new List<string>(input.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
        }
    }
}
