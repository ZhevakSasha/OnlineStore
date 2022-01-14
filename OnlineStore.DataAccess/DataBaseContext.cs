using OnlineStore.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;


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

    }
}
