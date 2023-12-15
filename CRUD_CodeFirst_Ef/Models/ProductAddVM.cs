﻿using CRUD_CodeFirst_Ef.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRUD_CodeFirst_Ef.Models
{
    public class ProductAddVM
    {
        [Required(ErrorMessage = "Product Name is required!")]
        [MinLength(5, ErrorMessage = "Product Name can't be less than 3 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Price can't be less than 0!")]
        public decimal Price { get; set; }

        [ValidateNever]
        [BindProperty]
        public IFormFile Photo { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ValidateNever]
        [BindProperty]
        public List<SelectListItem> Categories { get; set; }
    }
}
