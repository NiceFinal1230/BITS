using BITS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using BITS.Areas.Identity.Data;
using BITS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BITS.ViewModel;

namespace BITS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BITSContext _context;
        private UserManager<BITSUser> _userManager;

        public HomeController(ILogger<HomeController> logger, BITSContext context, UserManager<BITSUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Search(string search)
        {
            // Check if the search string is null
            if (search == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Initialize a new HomeViewModel instance
            HomeViewModel home = new();

            // Retrieve the list of products and their stocktake information
            var vm = await GetProducts();

            // Filter the products based on the search string (case insensitive)
            var test = vm.Where(i => i.Product.Name.ToLower().Contains(search.ToLower())).ToList();

            // Assign the filtered products to the Search property of the home view model
            home.Search = test;
            return View(home);
        }

        public async Task<List<ProductStocktakeViewModel>> GetProducts()
        {
            // Initialize a new list of ProductStocktakeViewModel
            List<ProductStocktakeViewModel> vm = new();

            // Retrieve the list of all products from the database
            var list = await _context.Product.ToListAsync();

            // Loop through each product in the list
            foreach (var p in list)
            {
                // Find the stocktake entry for the current product, if it exists
                var stock = await _context.Stocktake.FirstOrDefaultAsync(s => s.ProductId == p.ProductId);
                if (stock == null)
                {
                    stock = new();
                }

                // Add a new ProductStocktakeViewModel to the list, with the current product and its stocktake entry
                vm.Add(new() { Product = p, Stocktake = stock });
            }

            // Filter the list to include only products that do not have a BaseGameId
            vm = vm.Where(i => i.Product.BaseGameId == null).ToList();

            return vm;
        }

        public async Task<IActionResult> Index()
        {
            // Initialize a new HomeViewModel instance
            HomeViewModel home = new();

            // Retrieve the list of products
            var vm = await GetProducts();

            // Assign the last 5 products to the Fixed property of the home view model
            // Remove the last 5 products from the list
            home.Fixed = vm.TakeLast(5).ToList();
            vm = vm.SkipLast(5).ToList();

            // Order the remaining products by release date in descending order
            // Assign the first 5 products (new releases) to the NewRelease property of the home view model
            vm = vm.OrderByDescending(p => p.Product.ReleaseDate).ToList();
            home.NewRelease = vm.Take(5).ToList();
            vm = vm.Skip(5).ToList();

            // Order the remaining products by discount rate in descending order
            // Assign the first 5 products (mega sale items) to the MegaSale property of the home view model
            vm = vm.OrderByDescending(i => i.Stocktake.DiscountRate).ToList();
            home.MegaSale = vm.Take(5).ToList();
            vm = vm.Skip(5).ToList();

            // Assign the remaining products to the Explore property of the home view model
            home.Explore = vm;

            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
