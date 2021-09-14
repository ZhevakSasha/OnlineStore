using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace OnlineStore.DataAccess.AdoRepositoryImplementation
{
    /// <summary>
    /// AdoSaleRepository implementation
    /// </summary>
    public class AdoSaleRepository : ISaleRepository
    {
        /// <summary>
        /// The connection string that includes the source database name, 
        /// and other parameters needed to establish the initial connection. 
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Constructor for private string field _connectionString
        /// </summary>
        /// <param name="connectionString"></param>
        public AdoSaleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public IEnumerable<Sale> GetList()
        {
            var sales = new List<Sale>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Sales", connection);               
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var sale = new Sale()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        DateOfSale = reader["DateOfSale"].ToString(),
                        Amount = Convert.ToInt32(reader["Amount"])
                    };
                    sales.Add(sale);
                }
                return (sales);
            }
        }

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="id">Takes id parameter. </param>
        /// <returns>Return one object by id. </returns>
        public Sale GetEntity(int id)
        {
            var sale = new Sale();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Sales WHERE Id = {id}", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sale.Id = Convert.ToInt32(reader["Id"]);
                    sale.ProductId = Convert.ToInt32(reader["ProductId"]);
                    sale.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    sale.DateOfSale = reader["DateOfSale"].ToString();
                    sale.Amount = Convert.ToInt32(reader["Amount"]);
                }
                return sale;
            }
        }

        /// <summary>
        /// Create method.
        /// Creates an object of Sale class.
        /// </summary>
        /// <param name="sale">Takes an object of Sale class.</param>
        public void Create(Sale sale)
        {   
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Sales" +
                   "(ProductId, CustomerId, DateOfSale, Amount)" +
                   " VALUES(@ProductId, @CustomerId, @DateOfSale, @Amount)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@ProductId", sale.ProductId);
                command.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
                command.Parameters.AddWithValue("@DateOfSale", sale.DateOfSale);
                command.Parameters.AddWithValue("@Amount", sale.Amount);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Update method.
        /// Updates an object of Sale class.
        /// </summary>
        /// <param name="sale">Takes an object of Product class.</param>
        public void Update(Sale sale)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Sales SET ProductId = @ProductId," +
                                             "CustomerId = @CustomerId," +
                                             "DateOfSale = @DateOfSale," +
                                             "Amount = @Amount " +
                                             $"WHERE Id ={sale.Id}", connection);
                connection.Open();
                command.Parameters.AddWithValue("@ProductId", sale.ProductId);
                command.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
                command.Parameters.AddWithValue("@DateOfSale", sale.DateOfSale);
                command.Parameters.AddWithValue("@Amount", sale.Amount);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Delete method.
        /// Deletes an object of Sale class.
        /// </summary>
        /// <param name="sale">Takes an object of Sale class.</param>
        public void Delete(Sale sale)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand($"DELETE FROM Sales where Id = {sale.Id}", connection);
                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        public void Save()
        {

        }

    }
}
