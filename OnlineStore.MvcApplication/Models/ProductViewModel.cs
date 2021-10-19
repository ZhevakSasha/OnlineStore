﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MvcApplication.Models
{
    public class ProductViewModel
    {
        /// <summary>
        /// Property  for storing product id.
        /// </summary>
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// Property  for storing product name.
        /// </summary>
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product required")]
        [StringLength(30)]
        public string ProductName { get; set; }

        /// <summary>
        /// Property for storing price of product.
        /// </summary>
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price required")]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be less than a ten-digit number")]
        public int? Price { get; set; }

        /// <summary>
        /// Property for storing product unit of measurement.
        /// </summary>
        [Display(Name = "Unit of Measurement")]
        [Required(ErrorMessage = "Unit Of Measurement required")]
        [StringLength(20)]
        public string UnitOfMeasurement { get; set; }
    }
}
