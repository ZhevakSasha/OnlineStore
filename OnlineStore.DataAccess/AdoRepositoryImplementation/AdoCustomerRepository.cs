using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OnlineStore.DataAccess.AdoRepositoryImplementation
{
    /// <summary>
    /// AdoCustomerRepository implementation.
    /// </summary>
    public class AdoCustomerRepository : ICustomerRepository
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
        public AdoCustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
            
        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public IEnumerable<Customer> GetList()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Customers", connection);
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
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Customers WHERE Id = {id}", connection);
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
        /// <param name="customer">Takes an object of Customer class.</param>
        public void Create(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Customers" +
                   "(FirstName, LastName, Addres, PhoneNumber)" +
                   " VALUES(@FirstName, @LastName, @Addres, @PhoneNumber)", connection);
                connection.Open();
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
        /// <param name="customer">Takes an object of Customer class.</param>
        public void Update(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Customers SET FirstName = @FirstName," +
                                             "LastName = @LastName," +
                                             "Addres = @Addres," +
                                             "PhoneNumber = @PhoneNumber " +
                                             $"WHERE Id ={customer.Id}", connection);
                connection.Open();
                
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
        public void Delete(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand($"DELETE FROM Customers where Id = {Id} " , connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Save()
        {

        }
    }
}
