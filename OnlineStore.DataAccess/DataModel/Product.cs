using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.DataModel
{
    /// <summary>
    /// Data model for a Product table. 
    /// </summary>
    public class Product : ModelId
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string UnitOfMeasurement { get; set; }
        public List<Sale> Sales { get; set; } = new List<Sale>();
       
    }
}
