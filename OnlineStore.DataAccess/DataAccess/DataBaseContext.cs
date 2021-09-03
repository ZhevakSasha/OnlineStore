using System;
using System.Collections.Generic;
using System.Text;
using OnlineStore.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace OnlineStore.DataAccess.DataAccess
{
   public class DataBaseContext : DbContext 
    {
        protected DataBaseContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Def"].ConnectionString);
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

    }
}
