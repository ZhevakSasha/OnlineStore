using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using OnlineStore.Domain.Models;

namespace OnlineStore.DataAccess
{
   public class DataBaseContext : DbContext 
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base (options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=TestOnlineStore;User ID=sa;Password=pa55w0rd!;");
        }


        /// <summary>
        /// A set of Customer class objects that are stored in the database. 
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// A set of Product class objects that are stored in the database. 
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// A set of Sale class objects that are stored in the database. 
        /// </summary>
        public DbSet<Sale> Sales { get; set; }

        static readonly Random rndGen = new Random();

        static string GetRandom(string ch, int pwdLength)
        {
            
            char[] pwd = new char[pwdLength];
            for (int i = 0; i < pwd.Length; i++)
                pwd[i] = ch[rndGen.Next(ch.Length)];
            return new string(pwd);
        }

        static DateTime RandomDay()
        {
            DateTime start = new DateTime(2020, 1, 1);
            Random gen = new Random();
            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            while (true)
                return start.AddDays(gen.Next(range));
        }

        /// <summary>
        /// On model creating method.
        /// </summary>
        /// <param name="builder">builder</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedCustomers(builder);
            this.SeedProducts(builder);
            this.SeedSales(builder);
            //this.SeedProductSale(builder);
        }

        private void SeedCustomers(ModelBuilder builder)
        {
            List<Customer> customers = new List<Customer>();
            string chars = "qwertyuiopasdfghjklzxcvbnm0123456789";
            string numbers = "0123456789";
            for (int i = 0; i < 1000; i++)
            {
                customers.Add(new Customer
                {
                    Id = i+1,
                    FirstName = GetRandom(chars, 10),
                    LastName = GetRandom(chars, 10),
                    Address = GetRandom(chars, 10),
                    PhoneNumber = GetRandom(numbers, 10)
                });
            }
            builder.Entity<Customer>().HasData(
                customers.ToArray()
                ) ;
        }

        private void SeedProducts(ModelBuilder builder)
        {
            List<Product> products = new List<Product>();
            string chars = "qwertyuiopasdfghjklzxcvbnm0123456789";
            for (int i = 0; i < 1000; i++)
            {
                products.Add(new Product
                {
                    Id = i + 1,
                    Price = rndGen.Next(100, 2000),
                    ProductName = GetRandom(chars,10),
                    UnitOfMeasurement = "pc."
                });
            }
            builder.Entity<Product>().HasData(
                products.ToArray()
                );
        }

        private void SeedSales(ModelBuilder builder)
        {
            List<Sale> sales = new List<Sale>();
            for (int i = 0; i < 1000; i++)
            {
                sales.Add(new Sale
                {
                    Id = i + 1,
                    CustomerId = rndGen.Next(1,1000),
                    Amount = rndGen.Next(1, 20),
                    DateOfSale = RandomDay().ToString("dd'.'MM'.'yyyy")
                });
            }
            builder.Entity<Sale>().HasData(
                sales.ToArray()
                );
        }

        private void SeedProductSale(ModelBuilder builder)
        {
            List<ProductSale> productSales = new List<ProductSale>();

            for (int i = 1; i<1000; i++)
            {
                productSales.Add(new ProductSale
                {
                    SalesId = i,
                    ProductsId = rndGen.Next(1, 1000)
                });
                productSales.Add(new ProductSale
                {
                    SalesId = i,
                    ProductsId = rndGen.Next(1, 1000)
                });
            }

            builder.Entity<ProductSale>().HasData(
               productSales.ToArray()
               );

        }
    }
}
