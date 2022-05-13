using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.DataAccess.RepositoryPatterns;
using OnlineStore.Domain.Models;

namespace OnlineStore.DataAccess.DapperRepositoryImplementation
{
    public class DapperCustomerRepository : ICustomerRepository
    {
        private readonly DbConnection _connection;

        public DapperCustomerRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public PagedList<Customer> GetList(PageParameters pageParameters)
        {
            var query = @$"SELECT *
                           FROM Customers
                           ORDER BY [Id]
                           OFFSET {pageParameters.PageNumber * pageParameters.PageSize} ROWS
                           FETCH NEXT {pageParameters.PageSize} ROWS ONLY";

            var customers = _connection.Query<Customer>(query);

            return new PagedList<Customer>(customers.ToList(),
                _connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Customers"),
                pageParameters.PageNumber,
                pageParameters.PageSize);
        }

        public IList<Customer> GetList()
        {
            var query = @$"SELECT *
                           FROM Customers";

            var customers = _connection.Query<Customer>(query).ToList();

            return customers;
        }

        public Customer GetEntity(int id)
        {
            var query = $"SELECT * FROM Customers WHERE Id = @Id";
            var customer = _connection.QuerySingleOrDefault<Customer>(query, new { id });
            return customer;
            
        }

        public void Create(Customer item)
        {
            var query = "INSERT INTO Customers (FirstName, LastName, Address, PhoneNumber) VALUES (@FirstName, @LastName, @Address, @PhoneNumber)";
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", item.FirstName, DbType.String);
            parameters.Add("LastName", item.LastName, DbType.String);
            parameters.Add("Address", item.Address, DbType.String);
            parameters.Add("PhoneNumber", item.PhoneNumber, DbType.String);
            _connection.Execute(query, parameters);
        }

        public void Update(Customer item)
        {
            var query = "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, Address = @Address, PhoneNumber = @PhoneNumber WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", item.Id, DbType.Int32);
            parameters.Add("FirstName", item.FirstName, DbType.String);
            parameters.Add("LastName", item.LastName, DbType.String);
            parameters.Add("Address", item.Address, DbType.String);
            parameters.Add("PhoneNumber", item.PhoneNumber, DbType.String);
            _connection.Execute(query, parameters);
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM Customers WHERE Id = @Id";
            _connection.Execute(query, new { id });
        }
    }
}
