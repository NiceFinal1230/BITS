using BITS.Areas.Identity.Data;
using BITS.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;
public class Product
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
    public string ProductType { get; set; } = string.Empty;
    public string RefundType { get; set; } = string.Empty;

    [ValidateNever]
    public IList<Genre> Genres { get; set; }
    [ValidateNever]
    public IList<string> PreviewImages { get; set; } = new List<string>();
    [ValidateNever]
    public IList<Features> Features { get; set; }
    public DateTime ReleaseDate{ get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? OSMinimum { get; set; }
    public string? OSRecommended { get; set; }
    public string? ProcessorMinimum { get; set; }
    public string? ProcessorRecommended { get; set; }
    public string? MemoryMinimum { get; set; }
    public string? MemoryRecommended { get; set; }
    public string? StorageMinimum { get; set; }
    public string? StorageRecommended { get; set; }
    public string? GraphicsMinimum { get; set; }
    public string? GraphicsRecommended { get; set; }
    public double Price { get; set; }
    public int? BaseGameId { get; set; }

}


