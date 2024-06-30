using BITS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BITS.Controllers
{
    public class LibraryController : Controller
    {
        public IActionResult Index()
        {
            List<string> options = new List<string>()
            {
                "Title",
                "Date Purchased",
                "Oldest",
                "Recent"
            };

            ViewBag.Options = new SelectList(options);

            List<string> view = new List<string>()
            {
                "Favorite",
                "Completed",
                "Dropped",
                "All"
            };

            ViewBag.View = new SelectList(view);

            var libraries = new List<Library>
    {
            new Library { Id = 2, Name = "Alan Wake 2", ImageUrl = "/images/aw2.jpg", DateOfPurchase= DateTime.Now.AddDays(-1), Favorite = false },

            new Library { Id = 2, Name = "Ghost of Tsushima DIRECTOR'S CUT", ImageUrl = "/images/got.jpeg", DateOfPurchase= DateTime.Now.AddDays(-2), Favorite = false },

            new Library { Id = 2, Name = "Grand Theft Auto V: Premium Edition", ImageUrl = "/images/gtav.jpg", DateOfPurchase= DateTime.Now.AddDays(-3), Favorite = false },

            new Library { Id = 2, Name = "Hades", ImageUrl = "/images/h1.jpeg", DateOfPurchase= DateTime.Now.AddDays(-4), Favorite = false },

            new Library { Id = 2, Name = "Hades II", ImageUrl = "/images/h2.webp", DateOfPurchase= DateTime.Now.AddDays(-5), Favorite = false },

            new Library { Id = 2, Name = "Sleeping Dogs: Definitive Edition", ImageUrl = "/images/sd.jpg", DateOfPurchase= DateTime.Now.AddDays(-6), Favorite = false },

            new Library { Id = 1, Name = "The Elder Scrolls V: Skyrim Special Edition", ImageUrl = "/images/ess.jpg", DateOfPurchase = DateTime.Now.AddDays(-7), Favorite = true }
    };
            return View(libraries);
        }
    }
}
