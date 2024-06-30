using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;

public class Source
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string ExternalLink { get; set; }
    public string Types { get; set; }
}
