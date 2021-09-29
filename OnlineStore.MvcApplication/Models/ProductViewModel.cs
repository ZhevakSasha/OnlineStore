using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    public class ProductViewModel
    {
        /// <summary>
        /// Property  for storing product id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property  for storing product name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Property for storing price of product.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Property for storing product unit of measurement.
        /// </summary>
        public string UnitOfMeasurement { get; set; }
    }
}
