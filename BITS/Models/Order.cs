using BITS.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;

public class Order
{
    public int OrderId { get; set; }
    [Required(ErrorMessage = "Original price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public double OriginalPrice { get; set; }
    [Required(ErrorMessage = "Subtotal is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public double Subtotal { get; set; }
    [Required(ErrorMessage = "Discount price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public double DiscountPrice { get; set; }
    public string UserId { get; set; }
    [Required(ErrorMessage = "Street address is required.")]
    [StringLength(100, ErrorMessage = "Publisher name cannot be longer than 100 characters.")]
    public string StreetAddress { get; set; } = string.Empty;
    [Required(ErrorMessage = "Post code is required.")]
    public string PostCode { get; set; } = string.Empty;
    [Required(ErrorMessage = "Suburb is required.")]
    public string Suburb { get; set; } = string.Empty;
    [Required(ErrorMessage = "State is required.")]
    public string State { get; set; } = string.Empty;
    public DateTime? DateOfPurchase { get; set; } = DateTime.Now;
    public bool? Favorite { get; set; } = false;

}

