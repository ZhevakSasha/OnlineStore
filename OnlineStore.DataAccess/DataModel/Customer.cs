using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.DataModel
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Addres { get; set; }
        public string PhoneNumber { get; set; }

        public List<Sale> Sales { get; set; } = new List<Sale>();
    }
}
