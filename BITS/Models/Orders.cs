namespace BITS.Models;

public class Orders
{
    public int OrderId { get; set; }
    public User User { get; set; }
    public string StreetAddress { get; set; }
    public string PostCode { get; set; }
    public string Suburb { get; set; }
    public string State { get; set; }

}

public class ProductsInOrders
{
    public Orders OrderId { get; set; }
    public Product Products { get; set; }
    public int Quantity { get; set; }

}