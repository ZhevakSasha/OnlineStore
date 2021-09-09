using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;

namespace OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation
{
    class EntityFrameworkCustomerRepository : ICustomerRepository, IDisposable
    {
        private readonly DataBaseContext context;
        private readonly string connectionString;

        public EntityFrameworkCustomerRepository()
        {
            context = new DataBaseContext(connectionString);
        }

        public void Create(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public void Delete(Customer customer)
        {
            if (customer != null)
                context.Customers.Remove(customer);
        }

        public Customer GetEntity(int Id)
        {
            return context.Customers.Find(Id);
        }

        public IEnumerable<Customer> GetList()
        {
            return context.Customers;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}