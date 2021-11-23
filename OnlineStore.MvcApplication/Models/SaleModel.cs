using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    public class SaleModel
    {
        /// <summary>
        /// Property  for storing sale id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Property  for storing product id.
        /// </summary>
        [JsonProperty("productId")]
        public int? ProductId { get; set; }

        /// <summary>
        /// Property  for storing product name.
        /// </summary>
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Property  for storing customer id.
        /// </summary>
        [JsonProperty("customerId")]
        public int? CustomerId { get; set; }

        /// <summary>
        /// Property  for storing customer name.
        /// </summary>
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Property  for storing date of sale.
        /// </summary>
        [JsonProperty("dateOfSale")]
        public string DateOfSale { get; set; }

        /// <summary>
        /// Property  for storing amount of sales.
        /// </summary>
        [JsonProperty("amount")]
        public int? Amount { get; set; }
    }
}
