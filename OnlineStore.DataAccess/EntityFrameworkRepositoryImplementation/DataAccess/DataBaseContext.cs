using OnlineStore.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;


namespace OnlineStore.DataAccess.DataAccess
{
   public class DataBaseContext : DbContext 
    {
        /// <summary>
        /// The connection string that includes the source database name, 
        /// and other parameters needed to establish the initial connection. 
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Constructor for private string field _connectionString.
        /// </summary>
        /// <param name="connectionString"></param>
        public DataBaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Method for configuring connection strings for connecting to MS SQL Server.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
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
