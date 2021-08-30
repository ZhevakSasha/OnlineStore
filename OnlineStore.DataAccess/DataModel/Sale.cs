using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.DataModel
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string DateOfSale { get; set; }
        public int Amount { get; set; }
        public Product Products { get; set; }
        public Customer Customers { get; set; }

    }
}
