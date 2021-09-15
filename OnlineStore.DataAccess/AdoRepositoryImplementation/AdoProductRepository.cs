using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OnlineStore.DataAccess.AdoRepositoryImplementation
{
    /// <summary>
    /// AdoProductRepository implementation
    /// </summary>
    public class AdoProductRepository : IProductRepository
    {
        /// <summary>
        /// The connection string that includes the source database name, 
        /// and other parameters needed to establish the initial connection. 
        /// </summary>

        /// <summary>
        /// Constructor for private string field _connectionString
        /// </summary>
        /// <param name="connectionString"></param>
        private readonly string _connectionString;

        public AdoProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public IEnumerable<Product> GetList()
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Products", connection);              
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductName = reader["ProductName"].ToString(),
                        Price = Convert.ToInt32(reader["Price"]),
                        UnitOfMeasurement = reader["UnitOfMeasurement"].ToString()

                    };
                    products.Add(product);
                }
                return (products);
            }
        }

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="id">Takes id parameter. </param>
        /// <returns>Return one object by id. </returns>
        public Product GetEntity(int id)
        {
            var product = new Product();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Products WHERE Id = {id}", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    product.Id = Convert.ToInt32(reader["Id"]);
                    product.ProductName = reader["ProductName"].ToString();
                    product.Price = Convert.ToInt32(reader["Price"]);
                    product.UnitOfMeasurement = reader["UnitOfMeasurement"].ToString();
                }
                return product;
            }
        }

        /// <summary>
        /// Create method.
        /// Creates an object of Product class.
        /// </summary>
        /// <param name="product">Takes an object of Product class.</param>
        public void Create(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Products" +
                   "(ProductName, Price, UnitOfMeasurement)" +
                   " VALUES(@ProductName, @Price, @UnitOfMeasurement)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@UnitOfMeasurement", product.UnitOfMeasurement);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Update method.
        /// Updates an object of Product class.
        /// </summary>
        /// <param name="product">Takes an object of Product class.</param>
        public void Update(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Products SET ProductName = @ProductName," +
                                             "Price = @Price," +
                                             "UnitOfMeasurement = @UnitOfMeasurement " +
                                             $"WHERE Id ={product.Id}", connection);
                connection.Open();
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@UnitOfMeasurement", product.UnitOfMeasurement);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Delete method.
        /// Deletes an object of Product class.
        /// </summary>
        /// <param name="product">Takes an object of Product class.</param>
        public void Delete(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand($"DELETE FROM Products where Id = {product.Id}", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Save()
        {
             
        }
    }
}
