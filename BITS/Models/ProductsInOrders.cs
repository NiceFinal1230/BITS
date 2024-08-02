using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BITS.Models;
public class ProductsInOrders
{
    public int  ProductsInOrdersId { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int Products { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public int Quantity { get; set; }

}
