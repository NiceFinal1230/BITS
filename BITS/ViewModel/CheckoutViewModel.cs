using BITS.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.ViewModel;
public class CheckoutViewModel
{
    public List<ProductStocktakeViewModel> ProductStocktake { get; set; } = new List<ProductStocktakeViewModel>();
    public double Prices { get; set; }
    public double Discount { get; set; }
    public double Subtotal { get; set; }
    public int Quantity { get; set; }
    public string? CardNumber { get; set; } = string.Empty;
    public string? CardOwner { get; set; } = string.Empty;
    public string? Expiry { get; set; } = string.Empty;
    public string? CVV { get; set; } = string.Empty;

}

