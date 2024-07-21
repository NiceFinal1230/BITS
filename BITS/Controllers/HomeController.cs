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

        public async Task<IActionResult> Index()
        {
            List<ProductStocktakeViewModel> vm = new();
            HomeViewModel home = new();
            var list = await _context.Product.ToListAsync();
            foreach (var p in list)
            {
                //var stock = await _context.Stocktake.FirstOrDefaultAsync(s => s.ProductId == p.ProductId && s.Quantity > 0);
                var stock = await _context.Stocktake.FirstOrDefaultAsync(s => s.ProductId == p.ProductId);
                if(stock == null)
                {
                    stock = new();
                }
                vm.Add(new() { Product = p, Stocktake = stock });
            }

            home.Fixed = vm.TakeLast(5).ToList();
            vm = vm.SkipLast(5).ToList();

            home.NewRelease = vm.OrderByDescending(p => p.Product.ReleaseDate).Take(5).ToList();
            vm = vm.Skip(5).ToList();

            home.MegaSale = vm.OrderByDescending(i => i.Stocktake.DiscountRate).Take(5).ToList();
            vm = vm.Skip(5).ToList();

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
