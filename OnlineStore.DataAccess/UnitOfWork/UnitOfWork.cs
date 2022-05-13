using System;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using OnlineStore.DataAccess.RepositoryPatterns;

namespace OnlineStore.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        private ICustomerRepository _customerRepository;
        private IProductRepository _productRepository;
        private ISaleRepository _saleRepository;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers => _customerRepository ??= new EntityFrameworkCustomerRepository(_context);

        public IProductRepository Products => _productRepository ??= new EntityFrameworkProductRepository(_context);

        public ISaleRepository Sales => _saleRepository ??= new EntityFrameworkSaleRepository(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;

            if (disposing)
            {
                _context.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
