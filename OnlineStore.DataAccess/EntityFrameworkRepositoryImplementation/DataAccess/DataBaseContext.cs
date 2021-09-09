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
        private readonly string _connectionString;
        public DataBaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

    }
}
