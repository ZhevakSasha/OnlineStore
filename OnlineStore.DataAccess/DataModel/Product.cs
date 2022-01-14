using System.Collections.Generic;

namespace OnlineStore.DataAccess.DataModel
{
    /// <summary>
    /// Data model for a Product table. 
    /// </summary>
    public class Product : ModelBase
    {
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

        /// <summary>
        /// Property for getting List of sales.
        /// </summary>
        public List<Sale> Sales { get; set; } = new List<Sale>();
       
    }
}
