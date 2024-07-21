using BITS.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.ViewModel;
public class HomeViewModel
{
    public List<ProductStocktakeViewModel> Fixed { get; set; } = new List<ProductStocktakeViewModel>();
    public List<ProductStocktakeViewModel> NewRelease { get; set; } = new List<ProductStocktakeViewModel>();
    public List<ProductStocktakeViewModel> MegaSale { get; set; } = new List<ProductStocktakeViewModel>();
    public List<ProductStocktakeViewModel> Explore { get; set; } = new List<ProductStocktakeViewModel>();

}

