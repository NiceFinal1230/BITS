using BITS.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;

public class Order
{
    public int OrderId { get; set; }
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public double OriginalPrice { get; set; }
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public double Subtotal { get; set; }
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public double DiscountPrice { get; set; }
    public string UserId { get; set; }
    public string? StreetAddress { get; set; } = string.Empty;
    public string? PostCode { get; set; } = string.Empty;
    public string? Suburb { get; set; } = string.Empty;
    public string? State { get; set; } = string.Empty;
    public DateTime? DateOfPurchase { get; set; } = DateTime.Now;
    public bool? Favorite { get; set; } = false;

}

