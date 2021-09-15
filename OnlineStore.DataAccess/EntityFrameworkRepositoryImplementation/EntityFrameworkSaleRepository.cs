using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;

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
        private readonly DataBaseContext context;

        /// <summary>
        /// Constructor for private string field connectionString.
        /// </summary>
        /// <param name="connectionString"></param>
        public EntityFrameworkSaleRepository(string connectionString)
        {
            context = new DataBaseContext(connectionString);
        }

        /// <summary>
        /// Adds an object of Sale class in the database.
        /// </summary>
        /// <param name="sale"></param>
        public void Create(Sale sale)
        {
            context.Sales.Add(sale);
        }

        /// <summary>
        /// Deletes an object of Sale class.
        /// </summary>
        /// <param name="sale"></param>
        public void Delete(Sale sale)
        {
            if (sale != null)
                context.Sales.Remove(sale);
        }

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="Id">Takes id parameter</param>
        /// <returns>Return one object by id.</returns>
        public Sale GetEntity(int Id)
        {
            return context.Sales.Find(Id);
        }

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public IEnumerable<Sale> GetList()
        {
            return context.Sales;
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
        /// Updates an object of Sale class.
        /// </summary>
        /// <param name="sale">Takes an object of Sale class.</param>
        public void Update(Sale sale)
        {
            context.Sales.Update(sale);
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
