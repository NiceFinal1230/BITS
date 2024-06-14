using BITS.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.ViewModel;
public class CartViewModel
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string ProductType { get; set; }
    public string RefundType { get; set; }
    public string Price { get; set; }
    public string Picture { get; set; }
    public int Quantity { get; set; }

}

