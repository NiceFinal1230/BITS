using BITS.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.ViewModel;
public class OrdersViewModel
{
    public List<ProductStocktakeViewModel> ProductStocktake { get; set; } = new List<ProductStocktakeViewModel>();
    public HashSet<int> RunOut { get; set; } = new HashSet<int>();
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Prices { get; set; }
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Discount { get; set; }
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Subtotal { get; set; }
    public int Quantity { get; set; }
    public string? CardNumber { get; set; } = string.Empty;
    public string? CardOwner { get; set; } = string.Empty;
    public string? Expiry { get; set; } = string.Empty;
    public string? CVV { get; set; } = string.Empty;
    public Boolean IsCartItem { get; set; } = false;

}

