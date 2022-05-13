using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DataAccess.DapperRepositoryImplementation;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using OnlineStore.DataAccess.RepositoryPatterns;

namespace OnlineStore.DataAccess.UnitOfWork
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly DbConnection _connection;

        private DbTransaction _transaction;

        private DapperContext _context;

        private ICustomerRepository _customerRepository;

        private IProductRepository _productRepository;

        private ISaleRepository _saleRepository;

        public DapperUnitOfWork(DbConnection connection)
        {
            _connection = connection;
        }
        public DbConnection Connection => _connection;

        public DbTransaction Transaction => _transaction;

        public ICustomerRepository Customers => _customerRepository ??= new DapperCustomerRepository(_connection);

        public IProductRepository Products => _productRepository ??= new DapperProductRepository(_connection);

        public ISaleRepository Sales => _saleRepository ??= new DapperSaleRepository(_connection);

        public void Save()
        {
           
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public async Task BeginAsync()
        {
            _transaction = await _connection.BeginTransactionAsync();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

    }
}
