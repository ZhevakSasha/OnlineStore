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
    public class DapperProductRepository : IProductRepository
    {
        private readonly DbConnection _connection;

        public DapperProductRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public PagedList<Product> GetList(PageParameters pageParameters)
        {
            var query = @$"SELECT *
                           FROM Products
                           ORDER BY [Id]
                           OFFSET {pageParameters.PageNumber * pageParameters.PageSize} ROWS
                           FETCH NEXT {pageParameters.PageSize} ROWS ONLY";

            var products = _connection.Query<Product>(query);

            return new PagedList<Product>(products.ToList(),
                _connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Products"),
                pageParameters.PageNumber,
                pageParameters.PageSize);
        }

        public IList<Product> GetList()
        {
            var query = @$"SELECT *
                           FROM Products";

            var products = _connection.Query<Product>(query).ToList();

            return products;
        }

        public Product GetEntity(int id)
        {
            var query = $"SELECT * FROM Products WHERE Id = @Id";
            var product = _connection.QuerySingleOrDefault<Product>(query, new { id });
            return product;
        }

        public void Create(Product item)
        {
            var query = "INSERT INTO Products (ProductName, Price, UnitOfMeasurement) VALUES (@ProductName, @Price, @UnitOfMeasurement)";
            var parameters = new DynamicParameters();
            parameters.Add("ProductName", item.ProductName, DbType.String);
            parameters.Add("Price", item.Price, DbType.Int32);
            parameters.Add("UnitOfMeasurement", item.UnitOfMeasurement, DbType.String);
            _connection.Execute(query, parameters);
        }

        public void Update(Product item)
        {
            var query = "UPDATE Products SET ProductName = @ProductName, Price = @Price, UnitOfMeasurement = @UnitOfMeasurement WHERE Id = @Id)";
            var parameters = new DynamicParameters();
            parameters.Add("Id", item.Id, DbType.Int32);
            parameters.Add("ProductName", item.ProductName, DbType.String);
            parameters.Add("Price", item.Price, DbType.Int32);
            parameters.Add("UnitOfMeasurement", item.UnitOfMeasurement, DbType.String);
            _connection.Execute(query, parameters);
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM Products WHERE Id = @Id";
            _connection.Execute(query, new { id });
        }
    }
}
