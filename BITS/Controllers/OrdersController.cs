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
        // Initialize view models for the cart and orders
        CartViewModel model = new();
        OrdersViewModel vm = new();

        // Check if there is an error message in TempData and assign it to ViewBag
        if(TempData["errMsg"] != null)
        {
            ViewBag.errMsg = TempData["errMsg"]!.ToString();
        }

        // Retrieve the products from the session
        var products = HttpContext.Session.Get<CartViewModel>("products");
        if (products != null)
        {
            // If products are found, set IsCartItem to true and assign the products to the model
            vm.IsCartItem = true;
            model = products;
        }

        // Retrieve a single product from the session
        var single_product = HttpContext.Session.Get<Product>("product");
        if (single_product != null)
        {
            // Find the product in the context using the product ID
            var item = await _context.Product.FindAsync(single_product.ProductId);
            if (item != null)
            {
                // Check the stock for the product
                var stocktake = await _context.Stocktake.FirstOrDefaultAsync(x => x.ProductId == single_product.ProductId && x.Quantity > 0);
                if (stocktake == null)
                {
                    // If no stock is found, add the product ID to the RunOut list and add the product with an empty Stocktake object to the model
                    vm.RunOut.Add(single_product.ProductId);
                    model.ProductStocktake.Add(new ProductStocktakeViewModel { Product = item, Stocktake = new(), Quantity = 1 });
                }
                else 
                {
                    // If stock is found, add the product with the Stocktake object to the model
                    model.ProductStocktake.Add(new ProductStocktakeViewModel { Product = item, Stocktake = stocktake, Quantity = 1 }); 
                }
            }
        }

        // Retrieve the current user
        var user = await _userManager.GetUserAsync(User);

        // Assign the user ID to ViewBag
        ViewBag.UserId = user.Id;

        // If the model is not null, add the products and RunOut list to the orders view model
        if (model != null)
        {
            foreach (var product in model.ProductStocktake)
            {
                vm.ProductStocktake.Add(product);
            }
            vm.RunOut.AddRange(model.RunOut);
        }

        // Initialize variables for calculating prices and discounts
        double prices = 0;
        double discount = 0;
        double discountedPrice = 0;
        double subtotal = 0;

        // Calculate the total prices, discounts, and subtotal
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

        // Round the calculated values and assign them to the orders view model
        vm.Prices = Math.Round(prices, 2);
        vm.Discount = Math.Round(discount, 2);
        vm.Subtotal = Math.Round(subtotal, 2);

        // Store the orders view model in the session
        HttpContext.Session.Set<OrdersViewModel>("orders", vm);
        return View(vm);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        // Retrieve the orders model from the session
        var model = HttpContext.Session.Get<OrdersViewModel>("orders");
        if(model == null)
        {
            return NotFound();
        }

        // Get the current user
        var user = await _userManager.GetUserAsync(User);

        try
        {
            // Check if there is sufficient stock for each product in the order
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

            // Create a new order and save it to the database
            var order = new Order { UserId=user!.Id, OriginalPrice = model.Prices, DiscountPrice = model.Discount, Subtotal = model.Subtotal,
            StreetAddress = user.StreetAddress, Suburb = user.Suburb, PostCode = user.PostCode, State = user.State };
            _context.Order.Add(order);

            // Save changes to generate the order ID
            await _context.SaveChangesAsync();

            // Update stock quantities and add products to the order
            foreach (var item in model.ProductStocktake)
            {
                var id = item.Product.ProductId;
                var stock = await _context.Stocktake.FirstOrDefaultAsync(x => x.ProductId == id && x.Quantity >= item.Quantity);
                stock!.Quantity = stock.Quantity - item.Quantity;
                _context.Stocktake.Update(stock);
                _context.ProductsInOrders.Add(new () { OrderId=order.OrderId, Products=id, Quantity=item.Quantity});
            }
            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
        // If the order was created from items in the cart, remove the items from the cart
        if (model.IsCartItem)
        {
            var itemsToRemove = await _context.Cart.Where(x => x.UserId == user.Id).ToListAsync();
            _context.Cart.RemoveRange(itemsToRemove);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(controllerName: "Library", actionName: "Index");
    }


}
