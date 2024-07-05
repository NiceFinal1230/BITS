using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;

public class Source
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ExternalLink { get; set; } = string.Empty;
    // if string.Empty does not exist
    // when initialize database with seed data, it will crash
    public string Types { get; set; } = string.Empty;
}
