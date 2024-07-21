using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BITS.Data;
using BITS.Models;
using Microsoft.AspNetCore.Identity;
using BITS.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using BITS.ViewModel;
using System.Text.Json.Nodes;
using BITS.Services;
using System.Diagnostics;
using NuGet.Packaging;

namespace BITS.Controllers;
public class OrdersController : Controller
{
    private readonly BITSContext _context;
    private UserManager<BITSUser> _userManager;

    public OrdersController(BITSContext context, UserManager<BITSUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Checkout
    [Authorize]
    public async Task<IActionResult> Index()
    {
        CartViewModel model = new();
        OrdersViewModel vm = new();
        if(TempData["errMsg"] != null)
        {
            ViewBag.errMsg = TempData["errMsg"]!.ToString();
        }
        var products = HttpContext.Session.Get<CartViewModel>("products");
        if (products != null)
        {
            vm.IsCartItem = true;
            model = products;
        }
        var single_product = HttpContext.Session.Get<Product>("product");
        if (single_product != null)
        {
            var item = await _context.Product.FindAsync(single_product.ProductId);
            if (item != null)
            {
                var stocktake = await _context.Stocktake.FirstOrDefaultAsync(x => x.ProductId == single_product.ProductId && x.Quantity > 0);
                if (stocktake == null)
                {
                    vm.RunOut.Add(single_product.ProductId);
                    model.ProductStocktake.Add(new ProductStocktakeViewModel { Product = item, Stocktake = new(), Quantity = 1 });
                }
                else 
                {
                    model.ProductStocktake.Add(new ProductStocktakeViewModel { Product = item, Stocktake = stocktake, Quantity = 1 }); 
                }
            }
        }

        var user = await _userManager.GetUserAsync(User);

        ViewBag.UserId = user.Id;

        if (model != null)
        {
            foreach (var product in model.ProductStocktake)
            {
                vm.ProductStocktake.Add(product);
            }
            vm.RunOut.AddRange(model.RunOut);
        }

        double prices = 0;
        double discount = 0;
        double discountedPrice = 0;
        double subtotal = 0;

        foreach (var i in vm.ProductStocktake)
        {
            double originalPrice = i.Product.Price * i.Quantity;
            double discountRate = i.Stocktake.DiscountRate / 100;
            double currentDiscount = originalPrice * discountRate;
            prices += originalPrice; // Total original prices
            discount += currentDiscount; // Total discounts
            discountedPrice = originalPrice - currentDiscount; // Price after individual discount
            subtotal += discountedPrice; // Total after all individual discounts
        }

        vm.Prices = Math.Round(prices, 2);
        vm.Discount = Math.Round(discount, 2);
        vm.Subtotal = Math.Round(subtotal, 2);
        HttpContext.Session.Set<OrdersViewModel>("orders", vm);
        return View(vm);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create()
    {

        var model = HttpContext.Session.Get<OrdersViewModel>("orders");
        if(model == null)
        {
            return NotFound();
        }
        var user = await _userManager.GetUserAsync(User);
        try
        {
            foreach (var item in model.ProductStocktake)
            {
                var id = item.Product.ProductId;
                var stock = await _context.Stocktake.FirstOrDefaultAsync(x => x.ProductId == id && x.Quantity >= item.Quantity);
                if (stock == null)
                {
                    TempData["errMsg"] = "This product (" + item.Product.Name + ") does not have sufficient stock. ";
                    return RedirectToAction(nameof(Index));
                }
            }

            var order = new Order { UserId=user!.Id, OriginalPrice = model.Prices, DiscountPrice = model.Discount, Subtotal = model.Subtotal, StreetAddress = user.StreetAddress, Suburb = user.Suburb, PostCode = user.PostCode, State = user.State };
            _context.Order.Add(order);
            //must save the changes here, otherwise the order does not have an id
            await _context.SaveChangesAsync();
            foreach (var item in model.ProductStocktake)
            {
                var id = item.Product.ProductId;
                var stock = await _context.Stocktake.FirstOrDefaultAsync(x => x.ProductId == id && x.Quantity >= item.Quantity);
                stock!.Quantity = stock.Quantity - item.Quantity;
                _context.Stocktake.Update(stock);
                _context.ProductsInOrders.Add(new () { OrderId=order.OrderId, Products=id, Quantity=item.Quantity});
            }
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
        if (model.IsCartItem)
        {
            var itemsToRemove = await _context.Cart.Where(x => x.UserId == user.Id).ToListAsync();
            _context.Cart.RemoveRange(itemsToRemove);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(controllerName: "Library", actionName: "Index");
    }


}
