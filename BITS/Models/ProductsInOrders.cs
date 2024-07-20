using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BITS.Models;
public class ProductsInOrders
{
    public int  ProductsInOrdersId { get; set; }
    public int OrderId { get; set; }
    public int Products { get; set; }
    public int Quantity { get; set; }

}
