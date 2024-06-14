using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public Cart()
    {

    }
    public Cart(string _userId, int productId, int quantity)
    {
        UserId = _userId;
        ProductId = productId;
        Quantity = quantity;
    }
}
