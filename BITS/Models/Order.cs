using BITS.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;

public class Order
{
    public int OrderId { get; set; }
    public double OriginalPrice { get; set; }
    public double Subtotal { get; set; }
    public double DiscountPrice { get; set; }
    public string UserId { get; set; }
    public string? StreetAddress { get; set; } = string.Empty;
    public string? PostCode { get; set; } = string.Empty;
    public string? Suburb { get; set; } = string.Empty;
    public string? State { get; set; } = string.Empty;
    public DateTime? DateOfPurchase { get; set; } = DateTime.Now;
    public bool? Favorite { get; set; } = false;

}

