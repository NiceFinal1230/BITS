using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.Models;

public class Stocktake
{
    public int StocktakeId { get; set; }

    [Required(ErrorMessage = "Product ID is required.")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Source ID is required.")]
    public int SourceId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
    public int Quantity { get; set; } = 1;

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public double Price { get; set; } = 0.01;

    [Range(0, 100, ErrorMessage = "Discount Rate must be between 0 and 100.")]
    public double DiscountRate { get; set; } = 1;
/*
    [Required(ErrorMessage = "Discount Start Date is required.")]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "1/1/2000", "12/31/2099", ErrorMessage = "Discount Start Date must be between 01/01/2000 and 12/31/2099.")]*/
    public DateTime? DiscountStartDate { get; set; } = DateTime.Now;

/*    [Required(ErrorMessage = "Discount End Date is required.")]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "1/1/2000", "12/31/2099", ErrorMessage = "Discount End Date must be between 01/01/2000 and 12/31/2099.")]*/
    public DateTime? DiscountEndDate { get; set; } = DateTime.Now.AddYears(1);
}
