using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation
{
    /// <summary>
    /// EntityFrameworkSaleRepository implementation.
    /// </summary>
    public class EntityFrameworkSaleRepository : ISaleRepository, IDisposable
    {
        /// <summary>
        /// Context field.
        /// </summary>
        private readonly DataBaseContext _context;

        /// <summary>
        /// Constructor for private string field connectionString.
        /// </summary>
        /// <param name="connectionString"></param>
        public EntityFrameworkSaleRepository(DataBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an object of Sale class in the database.
        /// </summary>
        /// <param name="sale"></param>
        public void Create(Sale sale)
        {
            _context.Sales.Add(sale);
        }

        /// <summary>
        /// Deletes an object of Sale class.
        /// </summary>
        /// <param name="sale"></param>
        public void Delete(int Id)
        {
            var sale = _context.Sales.Local.First(c => c.Id == Id);
            _context.Sales.Remove(sale);
        }

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="Id">Takes id parameter</param>
        /// <returns>Return one object by id.</returns>
        public Sale GetEntity(int Id)
        {
            return _context.Sales.Find(Id);
        }

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public IEnumerable<Sale> GetList()
        {
            return _context.Sales;
        }

        /// <summary>
        /// Save changes to database.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Update method.
        /// Updates an object of Sale class.
        /// </summary>
        /// <param name="sale">Takes an object of Sale class.</param>
        public void Update(Sale sale)
        {
            _context.Sales.Update(sale);
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
