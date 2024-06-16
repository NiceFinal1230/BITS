using Microsoft.EntityFrameworkCore;

namespace BITS.Models;

[Keyless]
public class Discount
{
    public int StocktakeId { get; set; }
    public double Rate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
