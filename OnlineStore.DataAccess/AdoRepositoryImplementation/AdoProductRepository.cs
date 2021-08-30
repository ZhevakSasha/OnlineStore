using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OnlineStore.DataAccess.AdoRepositoryImplementation
{
    public class AdoProductRepository : IProductRepository
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=usersdb;Integrated Security=True";
        public IEnumerable<Product> GetList()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("spGetList", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
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

        public Product GetEntity(int id)
        {
            Product product = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("spGetProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
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

        public void Create(Product product)
        {
            using (SqlConnection connaction = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spCreateProduct", connaction);
                connaction.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@UnitOfMeasurement", product.UnitOfMeasurement);
                command.ExecuteNonQuery();
            }

        }

        public void Update(Product product)
        {
            using (SqlConnection connaction = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spUpdateProduct", connaction);
                connaction.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@UnitOfMeasurement", product.UnitOfMeasurement);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Product product)
        {
            using (SqlConnection connaction = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("spCreateProduct", connaction);
                command.CommandType = CommandType.StoredProcedure;
                connaction.Open();
                command.Parameters.AddWithValue("@Id", product.Id);
                command.ExecuteNonQuery();

            }
        }
        public void Save()
        {

        }
        public void Dispose()
        {

        }
    }
}
