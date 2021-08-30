using System;
using System.Collections.Generic;
using System.Text;
using OnlineStore.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.DataAccess.DataAccess
{
   public class DataBaseContext : DbContext 
    {
        protected DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

    }
}
