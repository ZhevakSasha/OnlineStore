using System.Collections.Generic;

namespace OnlineStore.Domain.Models
{
    /// <summary>
    /// Data model for Sales a table. 
    /// </summary>
    public class Sale : ModelBase
    {
        /// <summary>
        /// Property  for storing customer id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Property  for storing date of sale.
        /// </summary>
        public string DateOfSale { get; set; }

        /// <summary>
        /// Property  for storing amount of sales.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Property  for getting product.
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();

        //public List<ProductSale> ProductSale { get; set; } = new List<ProductSale>();

        /// <summary>
        /// Property  for getting customer.
        /// </summary>
        public Customer Customer { get; set; }

    }
}
