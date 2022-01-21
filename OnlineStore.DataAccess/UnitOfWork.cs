using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using System;

namespace OnlineStore.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private DataBaseContext _context;

        private EntityFrameworkCustomerRepository customerRepository;
        private EntityFrameworkProductRepository productRepository;
        private EntityFrameworkSaleRepository saleRepository;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }

        public EntityFrameworkCustomerRepository Customers
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new EntityFrameworkCustomerRepository(_context);
                return customerRepository;
            }
        }

        public EntityFrameworkProductRepository Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new EntityFrameworkProductRepository(_context);
                return productRepository;
            }
        }

        public EntityFrameworkSaleRepository Sales
        {
            get
            {
                if (saleRepository == null)
                    saleRepository = new EntityFrameworkSaleRepository(_context);
                return saleRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
