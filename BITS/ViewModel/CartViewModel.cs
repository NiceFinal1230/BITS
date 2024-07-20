﻿using BITS.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.ViewModel;
public class CartViewModel
{
    public List<ProductStocktakeViewModel> ProductStocktake { get; set; } = new List<ProductStocktakeViewModel>();
    public HashSet<int> RunOut { get; set; } = new HashSet<int>();
    public double Prices { get; set; }
    public double Discount { get; set; }
    public double Subtotal { get; set; }

}

