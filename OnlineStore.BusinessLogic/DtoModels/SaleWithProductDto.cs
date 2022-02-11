using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.DtoModels
{
    public class SaleWithProductDto
    {
        // Properties for sale

        /// <summary>
        /// Property for storing customer id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Property for storing customer name.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Property  for storing date of sale.
        /// </summary>
        public string DateOfSale { get; set; }

        /// <summary>
        /// Property  for storing amount of sales.
        /// </summary>
        public int Amount { get; set; }

        // Properties for Product

        public IList<ProductDto> Products { get; set; }
    }
}
