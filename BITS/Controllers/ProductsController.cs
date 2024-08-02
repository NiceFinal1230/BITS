
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BITS.Data;
using BITS.Models;
using BITS.ViewModel;
using BITS.Services;

namespace BITS.Controllers;

public class ProductsController : Controller
{
    private readonly BITSContext _context;

    // Constructor to initialize the database context
    public ProductsController(BITSContext context)
    {
        _context = context;
    }

    // Method to display a list of products, optionally filtered by genres or search term
    public async Task<IActionResult> Index(string[]? genres, string? search)
    {
        // Search by product name
        if (search != null)
        {
            // If search string is provided, search for products by their name
            var temp = await _context.Product.Where(product => product.Name.Contains(search)).ToListAsync();
            return View(temp);
        }

        // Search by genres
        if (genres != null && genres.Length > 0)
        {
            // Get the list of all products
            List<Product> list = await _context.Product.ToListAsync();

            // Initialize a list to store the filtered products
            List<Product> result = new();

            // Convert the genres array to a hash set for quick lookup
            HashSet<string> set = genres.ToHashSet();

            // Check if any of the product's genres match the provided genres
            // If a match is found, add the product to the result list and break the loop
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

        // If no search string or genres are provided, return the view with all products
        return View(await _context.Product.ToListAsync());
    }
    
    // Method to display add-ons for a base game product
    public async Task<IActionResult> AddOn(int? id)
    {
        // Check if the provided id is null
        if (id == null)
        {
            return NotFound();
        }

        // Retrieve the list of products that have the given id as their BaseGameId
        var products = await (from _addon in _context.Product
                      where _addon.BaseGameId == id
                      select _addon).ToListAsync();

        // Check if no products are found for the given BaseGameId
        if (products == null)
        {
            return NotFound();
        }

        // Initialize a list to store the ProductStocktakeViewModel objects
        List<ProductStocktakeViewModel> vm = new();
        foreach(var product in products)
        {
            var result = _context.Stocktake.FirstOrDefault(i => i.ProductId == product.ProductId);
            // Add a new ProductStocktakeViewModel to the list with the product and its stocktake details
            // If no stocktake details are found, add a new ProductStocktakeViewModel with an empty stocktake
            if (result != null)
                vm.Add(new ProductStocktakeViewModel { Product = product, Stocktake = result });
            else
            {
                vm.Add(new ProductStocktakeViewModel { Product = product, Stocktake = new() });
            }
        }

        // Retrieve the base product details using the provided id
        var baseProduct = await _context.Product.FindAsync(id);

        // Check if the base product is found
        if (baseProduct != null)
        {
            // Set the base product details in the ViewBag to be used in the view
            ViewBag.BaseProduct = baseProduct;
        }

        return View(vm);
    }

    // Method to display details of a specific product
    public async Task<IActionResult> Details(int? id)
    {
        // Check if the product ID is null
        if (id == null)
        {
            return NotFound();
        }

        // Find the product in the database using the provided ID without tracking
        var product = await _context.Product .AsNoTracking() .FirstOrDefaultAsync(m => m.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }

        // Retrieve stock details for the product and store them in ViewBag
        ViewBag.Stocktake = await _context.Stocktake .Where(stock => stock.ProductId == id) .ToListAsync();

        // Retrieve stock details ordered by price in descending order
        var stocks = await _context.Stocktake .Where(stock => stock.ProductId == id) .OrderByDescending(stock => stock.Price) .ToListAsync();

        // If there are any stock details available, set discount-related details in ViewBag
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

    // Method to display the product creation form with prepopulated data
    public IActionResult Create()
    {
        var product = new Product { Name = "Starcraft 2", Developer = "Arrowhead Game Studios", Publisher = "Play Station Plubishing LLC", Description = "The Galaxy’s Last Line of Offence. Enlist in the Helldivers and join the fight for freedom across a hostile galaxy in a fast, frantic, and ferocious third-person shooter.", Genres = new List<Genre> { Genre.Action, Genre.Shooter }, PreviewImages = new List<string> { "https://reservoircreative.studio/app/uploads/2023/09/rsvr-sony-keyart_helldivers2_preorderversion_web.jpg", "https://assets-prd.ignimgs.com/2023/09/14/vlcsnap-2023-09-14-16h24m15s013-1694726664032.png", "https://media.bleacherreport.com/image/upload/c_fill,g_faces,w_3800,h_2000,q_95/v1707834714/jig2wigcxan0jzfbjzax.jpg", "https://www.dexerto.com/cdn-image/wp-content/uploads/2024/02/20/helldivers-2-how-to-reinforce-teamates.jpg?width=3840&quality=60&format=auto", "https://lh7-us.googleusercontent.com/5DL4Kr8JUrOtL-ILUkFBWFC_tbvXSN09g7qWxYb341G8uGfPmdd0S1vDqhp1uAnPKN7F9EAmPN_XUh7U6x4cx2b4MVDf8AQGoKPbUbnY3k4VB1ujpJ6osa2aeAKU73L9tKe3g1NGZ2Xy3W9krCgafQ", "https://www.gameshub.com/wp-content/uploads/sites/5/2024/02/helldivers-2-gameplay.jpeg", "https://cdn.gamerbraves.com/2024/02/Helldivers-2-Gameplay-Screenshot-03.jpg" }, Features = new List<Features> { Features.SinglePlayer, Features.CloudSaves, Features.Multiplayer, Features.Coop }, ReleaseDate = new DateTime(2024, 02, 08), LastUpdated = DateTime.Now, OSMinimum = "Windows 10", OSRecommended = "Windows 10", ProcessorMinimum = "Intel Core i7-4790K or AMD Ryzen 5 1500X", ProcessorRecommended = "Intel Core i7-9700K or AMD Ryzen 7 3700X", MemoryMinimum = "8 GB RAM", MemoryRecommended = "16 GB RAM", StorageMinimum = "100 GB available space", StorageRecommended = "100 GB available space", GraphicsMinimum = "NVIDIA GeForce GTX 1050 Ti or AMD Radeon RX 470", GraphicsRecommended = "NVIDIA GeForce RTX 2060 or AMD Radeon RX 6600XT", ProductType = "Base Game", RefundType = "Self-Refundable", Price = 54.90 };
        PopulateAssignedGenresData(product);
        PopulateAssignedFeaturesData(product);
        PopulatePreviewImages(product);
        return View(product);
    }

    // Method to handle the product creation form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Product product, string _previewImages, string[] selectedFeatures, string[] selectedGenres)
    {
        // Set the selected genres for the product
        SetGenres(product, selectedGenres);

        // Set the selected features for the product
        SetFeatures(product, selectedFeatures);

        // Set the preview images for the product
        SetPreviewImages(product, _previewImages);

        // Check if the model state is valid
        if (ModelState.IsValid)
        {
            // Add the product to the database context
            _context.Add(product);

            // Save changes to the database asynchronously
            await _context.SaveChangesAsync();

            // Redirect to the CloseIFrame action method
            return RedirectToAction(nameof(CloseIFrame));
        }

        // If model state is not valid, populate the assigned genres and features data for the view
        PopulateAssignedGenresData(product);
        PopulateAssignedFeaturesData(product);
        return View(product);
    }

    
    // Method to close the iframe after a product is created or edited
    public IActionResult CloseIFrame(int? id)
    {
        return View();
    }

    // Method to display the product edit form
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

    // Method to handle the product edit form submission
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
                return RedirectToAction(nameof(CloseIFrame));
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
        PopulateAssignedFeaturesData(product);
        PopulateAssignedGenresData(product);
        PopulatePreviewImages(product);
        ViewBag.sources = _context.Source.ToList();
        ViewBag.Context = _context;
        ViewBag.Stocktake = await _context.Stocktake.Where(stock => stock.ProductId == id).ToListAsync();
        Stocktake stocktake = new Stocktake { ProductId = product.ProductId };
        return View(new ProductStocktakeViewModel { Product = product, Stocktake = stocktake });
    }

    // Method to handle the product delete form submission from product details page
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product != null)
        {
            _context.Product.Remove(product);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(CloseIFrame));
    }

    // Method to handle the Buy Now form submission from product details page
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

    // Method to handle the product delete form submission from admin index page
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
    // Method to check if a product exists
    private bool ProductExists(int id)
    {
        return _context.Product.Any(e => e.ProductId == id);
    }
    // Method to populate assigned genres data for the view
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
    // Method to populate assigned features data for the view
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
    // Method to populate preview images in a list for the view
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
    // Method to set genres for a product
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

    // Method to set features for a product
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
    // Method to set preview images for a product
    private void SetPreviewImages(Product product, string input)
    {
        if (input == null)
        {
            return;
        }
        product.PreviewImages = new List<string>(input.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
    }
}
