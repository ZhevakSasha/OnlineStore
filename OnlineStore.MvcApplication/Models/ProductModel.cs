using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Property  for storing product id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Property  for storing product name.
        /// </summary>
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Property for storing price of product.
        /// </summary>
        [JsonProperty("price")]
        public int? Price { get; set; }

        /// <summary>
        /// Property for storing product unit of measurement.
        /// </summary>
        [JsonProperty("unitOfMeasurement")]
        public string UnitOfMeasurement { get; set; }
    }
}
