using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MvcApplication.Models
{
    public class SaleViewModel
    {
        /// <summary>
        /// Property  for storing sale id.
        /// </summary>
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// Property  for storing product id.
        /// </summary>
        [BindRequired]
        public int? ProductId { get; set; }

        /// <summary>
        /// Property  for storing product name.
        /// </summary>
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Property  for storing customer id.
        /// </summary>
        [BindRequired]
        public int? CustomerId { get; set; }

        /// <summary>
        /// Property  for storing customer name.
        /// </summary>
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Property  for storing date of sale.
        /// </summary>
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Data required")]
        public string DateOfSale { get; set; }

        /// <summary>
        /// Property  for storing amount of sales.
        /// </summary>
        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Amount required")]
        [Range(1, 100, ErrorMessage = "AmountRange")]
        public int? Amount { get; set; }
    }
}
