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
    /// AdoCustomerRepository implementation
    /// </summary>
    class AdoCustomerRepository : ICustomerRepository
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
        public IEnumerable<Customer> GetList()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spGetCustomerList", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var customer = new Customer()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Addres = reader["Addres"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    };
                    customers.Add(customer);
                }
                return (customers);
            }
        }

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="id">Takes id parameter. </param>
        /// <returns>Return one object by id. </returns>
        public Customer GetEntity(int id)
        {
            var customer = new Customer();
            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("spGetCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    customer.Id = Convert.ToInt32(reader["Id"]);
                    customer.FirstName = reader["FirstName"].ToString();
                    customer.LastName = reader["LastName"].ToString();
                    customer.Addres = reader["Addres"].ToString();
                    customer.PhoneNumber = reader["PhoneNumber"].ToString();
                }
                return customer;
            }
        }

        /// <summary>
        /// Create method.
        /// Creates an object of Customer class.
        /// </summary>
        /// <param name="customer">Takes an object of Product class.</param>
        public void Create(Customer customer)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spCreateCustomer", connection);
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Addres", customer.Addres);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Update method.
        /// Updates an object of Customer class.
        /// </summary>
        /// <param name="customer">Takes an object of Product class.</param>
        public void Update(Customer customer)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spUpdateCustomer", connection);
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Addres", customer.Addres);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Delete method.
        /// Deletes an object of Customer class.
        /// </summary>
        /// <param name="customer">Takes an object of Customer class.</param>
        public void Delete(Customer customer)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spDeleteCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@Id", customer.Id);
                command.ExecuteNonQuery();

            }
        }

        public void Save()
        {

        }
    }
}
