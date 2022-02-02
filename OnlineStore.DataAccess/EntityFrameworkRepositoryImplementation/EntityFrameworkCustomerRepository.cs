using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.DataAccess.RepositoryPatterns;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly DataBaseContext _context;

        /// <summary>
        /// Constructor for private string field connectionString.
        /// </summary>
        /// <param name="connectionString"></param>
        public EntityFrameworkCustomerRepository(DataBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an object of Customer class in the database.
        /// </summary>
        /// <param name="customer"></param>
        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        /// <summary>
        /// Deletes an object of Customer class.
        /// </summary>
        /// <param name="customer"></param>
        public void Delete(int Id)
        {
            var customer = _context.Customers.First(c => c.Id == Id);
            _context.Customers.Remove(customer);
        }

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="Id">Takes id parameter</param>
        /// <returns>Return one object by id.</returns>
        public Customer GetEntity(int Id)
        {
            return _context.Customers.Find(Id);
        }

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public PagedList<Customer> GetList(PageParameters pageParameters)
        {
            return PagedList<Customer>.ToPagedList(_context.Customers,
                pageParameters.PageNumber,
                pageParameters.PageSize);
        }

        /// <summary>
        /// Update method.
        /// Updates an object of Customer class.
        /// </summary>
        /// <param name="customer">Takes an object of Customer class.</param>
        public void Update(Customer customer)
        {
            var entity = _context.Customers.Find(customer.Id);

            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.PhoneNumber = customer.PhoneNumber;
            entity.Address = customer.Address;

            _context.Customers.Update(entity);
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