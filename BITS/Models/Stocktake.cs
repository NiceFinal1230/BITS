using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;

public class Stocktake
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ItemId { get; set; }
    public Product? ProductId { get; set; }
    public Source? SourceId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

}
