namespace BITS.Models;

public class ProductsInOrders
{
    public Orders OrderId { get; set; }
    public Product Products { get; set; }
    public int Quantity { get; set; }

}
