using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace OnlineStore.DataAccess.AdoRepositoryImplementation
{
    /// <summary>
    /// AdoSaleRepository implementation
    /// </summary>
    class AdoSaleRepository : ISaleRepository
    {
        /// <summary>
        /// The connection string that includes the source database name, 
        /// and other parameters needed to establish the initial connection. 
        /// </summary>
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public IEnumerable<Sale> GetList()
        {
            var sales = new List<Sale>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spGetSaleList", connection);
                command.CommandType = CommandType.StoredProcedure;
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
            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("spGetSale", connection);
                command.CommandType = CommandType.StoredProcedure;
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
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spSaleCustomer", connection);
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
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
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spUpdateSale", connection);
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", sale.ProductId);
                command.Parameters.AddWithValue("@LastName", sale.CustomerId);
                command.Parameters.AddWithValue("@Addres", sale.DateOfSale);
                command.Parameters.AddWithValue("@PhoneNumber", sale.Amount);
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
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spDeleteSale", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@Id", sale.Id);
                command.ExecuteNonQuery();

            }
        }

        public void Save()
        {

        }

    }
}
