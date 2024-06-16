using BITS.Areas.Identity.Data;

namespace BITS.Models;

public class Orders
{
    public int OrderId { get; set; }
    public string UserId { get; set; }
    public string StreetAddress { get; set; }
    public string PostCode { get; set; }
    public string Suburb { get; set; }
    public string State { get; set; }

}

