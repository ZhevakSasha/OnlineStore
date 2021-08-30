using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.DataModel
{
    public class Product
    {
        public int Id { get; set; }
        public string PruductName { get; set; }
        public int Price { get; set; }
        public string UnitOfMeasurement { get; set; }
        public List<Sale> Sales { get; set; } = new List<Sale>();

    }
}
