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
using BITS.Services;
using Microsoft.AspNetCore.Http;
using System.Globalization;

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
                ViewBag.DiscountPrice = (product.Price * (100 - firstStock.DiscountRate) / 100).ToString("F2");
                ViewBag.StartDate = firstStock.DiscountStartDate;
                ViewBag.EndDate = firstStock.DiscountEndDate;
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var product = new Product { Name = "Starcraft 2", Developer = "Arrowhead Game Studios", Publisher = "Play Station Plubishing LLC", Description = "The Galaxy’s Last Line of Offence. Enlist in the Helldivers and join the fight for freedom across a hostile galaxy in a fast, frantic, and ferocious third-person shooter.", Genres = new List<Genre> { Genre.Action, Genre.Shooter }, PreviewImages = new List<string> { "https://reservoircreative.studio/app/uploads/2023/09/rsvr-sony-keyart_helldivers2_preorderversion_web.jpg", "https://assets-prd.ignimgs.com/2023/09/14/vlcsnap-2023-09-14-16h24m15s013-1694726664032.png", "https://media.bleacherreport.com/image/upload/c_fill,g_faces,w_3800,h_2000,q_95/v1707834714/jig2wigcxan0jzfbjzax.jpg", "https://www.dexerto.com/cdn-image/wp-content/uploads/2024/02/20/helldivers-2-how-to-reinforce-teamates.jpg?width=3840&quality=60&format=auto", "https://lh7-us.googleusercontent.com/5DL4Kr8JUrOtL-ILUkFBWFC_tbvXSN09g7qWxYb341G8uGfPmdd0S1vDqhp1uAnPKN7F9EAmPN_XUh7U6x4cx2b4MVDf8AQGoKPbUbnY3k4VB1ujpJ6osa2aeAKU73L9tKe3g1NGZ2Xy3W9krCgafQ", "https://www.gameshub.com/wp-content/uploads/sites/5/2024/02/helldivers-2-gameplay.jpeg", "https://cdn.gamerbraves.com/2024/02/Helldivers-2-Gameplay-Screenshot-03.jpg" }, Features = new List<Features> { Features.SinglePlayer, Features.CloudSaves, Features.Multiplayer, Features.Coop }, ReleaseDate = new DateTime(2024, 02, 08), LastUpdated = DateTime.Now, OSMinimum = "Windows 10", OSRecommended = "Windows 10", ProcessorMinimum = "Intel Core i7-4790K or AMD Ryzen 5 1500X", ProcessorRecommended = "Intel Core i7-9700K or AMD Ryzen 7 3700X", MemoryMinimum = "8 GB RAM", MemoryRecommended = "16 GB RAM", StorageMinimum = "100 GB available space", StorageRecommended = "100 GB available space", GraphicsMinimum = "NVIDIA GeForce GTX 1050 Ti or AMD Radeon RX 470", GraphicsRecommended = "NVIDIA GeForce RTX 2060 or AMD Radeon RX 6600XT", ProductType = "Base Game", RefundType = "Self-Refundable", Price = 54.90 };
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyNow(int? ProductId)
        {
            if(ProductId != null)
            {
                var p = await _context.Product.FindAsync(ProductId);
                if (p != null)
                {
                    HttpContext.Session.Set<Product>("product", p);
                }
            }
            HttpContext.Session.Remove("products");
            return RedirectToAction(controllerName: "Orders", actionName: "Index");
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
