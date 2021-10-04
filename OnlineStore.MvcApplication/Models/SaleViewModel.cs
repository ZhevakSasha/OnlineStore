using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    public class SaleViewModel
    {
        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        /// <summary>
        /// Property  for storing product name.
        /// </summary>
        public string ProductName { get; set; }

        public string CustomerName { get; set; }

        /// <summary>
        /// Property  for storing date of sale.
        /// </summary>
        public string DateOfSale { get; set; }

        /// <summary>
        /// Property  for storing amount of sales.
        /// </summary>
        public int Amount { get; set; }
    }
}
