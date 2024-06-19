﻿using BITS.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BITS.ViewModel;
public class ProductStocktakeViewModel
{
    public Product Product { get; set; } = new();
    public Stocktake Stocktake { get; set; } = new();


}

