using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;

namespace OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation
{
    /// <summary>
    /// EntityFrameworkCustomerRepository implementation.
    /// </summary>
    public class EntityFrameworkCustomerRepository : ICustomerRepository, IDisposable
    {
        /// <summary>
        /// Context field.
        /// </summary>
        private readonly DataBaseContext context;

        /// <summary>
        /// Constructor for private string field connectionString.
        /// </summary>
        /// <param name="connectionString"></param>
        public EntityFrameworkCustomerRepository(string connectionString)
        {
            context = new DataBaseContext(connectionString);
        }

        /// <summary>
        /// Creates an object of Customer class.
        /// </summary>
        /// <param name="customer"></param>
        public void Create(Customer customer)
        {
                context.Customers.Add(customer);
        }

        /// <summary>
        /// Deletes an object of Customer class.
        /// </summary>
        /// <param name="customer"></param>
        public void Delete(Customer customer)
        {
            if (customer != null)
                context.Customers.Remove(customer);
        }

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="Id">Takes id parameter</param>
        /// <returns>Return one object by id.</returns>
        public Customer GetEntity(int Id)
        {
            return context.Customers.Find(Id);
        }

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public IEnumerable<Customer> GetList()
        {
            return context.Customers;
        }

        /// <summary>
        /// Save changes to database.
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Update method.
        /// Updates an object of Customer class.
        /// </summary>
        /// <param name="customer">Takes an object of Customer class.</param>
        public void Update(Customer customer)
        {
            context.Customers.Update(customer);
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