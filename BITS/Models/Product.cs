using BITS.Areas.Identity.Data;
using BITS.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;
public class Product
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Developer name is required.")]
    [StringLength(100, ErrorMessage = "Developer name cannot be longer than 100 characters.")]
    public string Developer { get; set; }

    [Required(ErrorMessage = "Publisher name is required.")]
    [StringLength(100, ErrorMessage = "Publisher name cannot be longer than 100 characters.")]
    public string Publisher { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Product type is required.")]
    public string ProductType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Refund type is required.")]
    public string RefundType { get; set; } = string.Empty;

    [ValidateNever]
    public IList<Genre> Genres { get; set; }

    [ValidateNever]
    public IList<string> PreviewImages { get; set; } = new List<string>();

    [ValidateNever]
    public IList<Features> Features { get; set; }

    [Required(ErrorMessage = "Release date is required.")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    public string? LastUpdatedBy { get; set; }

    [Required(ErrorMessage = "Last updated date is required.")]
    [DataType(DataType.Date)]
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

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public double Price { get; set; }

    public int? BaseGameId { get; set; }
}


