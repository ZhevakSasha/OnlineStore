using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.DataAccess.RepositoryPatterns;
using OnlineStore.Domain.Models;

namespace OnlineStore.DataAccess.DapperRepositoryImplementation
{
    public class DapperSaleRepository : ISaleRepository
    {
       // private readonly DapperContext _context;
        private readonly DbConnection _connection;

        public DapperSaleRepository(DbConnection connection)
        {
            _connection = connection;
        }


        public PagedList<Sale> GetList(PageParameters pageParameters)
        {
            var query = @$"SELECT *
                           FROM Sales s
                           INNER JOIN Customers c ON s.CustomerId = c.Id
                           INNER JOIN ProductSale ps ON ps.SalesId = s.Id
                           INNER JOIN Products p ON p.Id = ps.ProductsId
                           ORDER BY s.Id
                           OFFSET {pageParameters.PageNumber * pageParameters.PageSize} ROWS
                           FETCH NEXT {pageParameters.PageSize} ROWS ONLY";
            
            var sales = _connection.Query<Sale, Customer, Product, Sale>(query, (sale, customer, product) => { sale.Customer = customer;
                sale.Products = new List<Product> {product};
                return sale;
            });

            var result = sales.GroupBy(s => s.Id).Select(g =>
            {
                var groupedSale = g.First();
                groupedSale.Products = g.Select(s => s.Products.Single()).ToList();
                return groupedSale;
            }).ToList();

            return new PagedList<Sale>(result,
                    _connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Sales"),
                    pageParameters.PageNumber,
                    pageParameters.PageSize);
        }

        public IList<Sale> GetList()
        {
            var query = @$"SELECT *
                           FROM Sales s
                           INNER JOIN Customers c ON s.CustomerId = c.Id
                           INNER JOIN ProductSale ps ON ps.SalesId = s.Id
                           INNER JOIN Products p ON p.Id = ps.ProductsId";

            var sales = _connection.Query<Sale, Customer, Product, Sale>(query, (sale, customer, product) => {
                sale.Customer = customer;
                sale.Products = new List<Product> { product };
                return sale;
            });

            var result = sales.GroupBy(s => s.Id).Select(g =>
            {
                var groupedSale = g.First();
                groupedSale.Products = g.Select(s => s.Products.Single()).ToList();
                return groupedSale;
            }).ToList();

            return result;
        }

        public Sale GetEntity(int id)
        {
            var query = @$"SELECT *
                           FROM Sales s
                           INNER JOIN Customers c ON s.CustomerId = c.Id
                           INNER JOIN ProductSale ps ON ps.SalesId = s.Id
                           INNER JOIN Products p ON p.Id = ps.ProductsId
                           WHERE s.Id = {id}";

            var sale = _connection.Query<Sale, Customer, Product, Sale>(query, (sale, customer, product) => {
                sale.Customer = customer;
                sale.Products = new List<Product> {product};
                return sale;
            });

            var result = sale.GroupBy(s => s.Id).Select(g =>
            {
                var groupedSale = g.First();
                groupedSale.Products = g.Select(s => s.Products.Single()).ToList();
                return groupedSale;
            }).First();

            return result;
        }

        public void Create(Sale item)
        {
            var query = "INSERT INTO Sales (CustomerId, DateOfSale, Amount) OUTPUT INSERTED.Id VALUES (@CustomerId, @DateOfSale, @Amount)";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerId", item.CustomerId, DbType.Int32);
            parameters.Add("DateOfSale", item.DateOfSale, DbType.String);
            parameters.Add("Amount", item.Amount, DbType.Int32);
            var id = _connection.QuerySingle<int>(query, parameters);
            
            foreach (var product in item.Products)
            {
                query = $"INSERT INTO ProductSale (ProductsId, SalesId) VALUES ({product.Id}, {id})";
                _connection.Execute(query);
            }
        }

        public void Update(Sale item)
        {
            var query = "UPDATE Sales SET CustomerId = @CustomerId, DateOfSale = @DateOfSale, Amount = @Amount WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", item.Id, DbType.Int32);
            parameters.Add("CustomerId", item.CustomerId, DbType.Int32);
            parameters.Add("DateOfSale", item.DateOfSale, DbType.String);
            parameters.Add("Amount", item.Amount, DbType.Int32);
            _connection.Execute(query, parameters);

            query = "DELETE FROM ProductSale WHERE SalesId = @Id";
            _connection.Execute(query, new { item.Id });

            foreach (var product in item.Products)
            {
                query = $"INSERT INTO ProductSale (ProductsId, SalesId) VALUES ({product.Id}, {item.Id})";
                _connection.Execute(query);
            }
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM Sales WHERE Id = @Id";
            _connection.Execute(query, new { id });

            query = "DELETE FROM ProductSale WHERE SalesId = @Id";
            _connection.Execute(query, new { id });
        }
    }
}
