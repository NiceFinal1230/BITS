using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;

public class Stocktake
{
    public int StocktakeId { get; set; }
    public int ProductId { get; set; }
    public int SourceId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double DiscountRate { get; set; }
    public DateTime DiscountStartDate { get; set; }
    public DateTime DiscountEndDate { get; set; }
}
