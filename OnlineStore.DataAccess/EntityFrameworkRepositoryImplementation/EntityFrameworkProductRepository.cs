using OnlineStore.DataAccess.PagedList;
using OnlineStore.DataAccess.RepositoryPatterns;
using OnlineStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation
{
    /// <summary>
    /// EntityFrameworkProductRepository implementation.
    /// </summary>
    public class EntityFrameworkProductRepository : IProductRepository, IDisposable
    {
        /// <summary>
        /// Context field.
        /// </summary>
        private readonly DataBaseContext _context;

        /// <summary>
        /// Constructor for private string field connectionString.
        /// </summary>
        /// <param name="connectionString"></param>
        public EntityFrameworkProductRepository(DataBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an object of Product class in the database.
        /// </summary>
        /// <param name="product"></param>
        public void Create(Product product)
        {
            _context.Products.Add(product);
        }

        /// <summary>
        /// Deletes an object of Product class.
        /// </summary>
        /// <param name="product"></param>
        public void Delete(int Id)
        {
            var product = _context.Products.First(c => c.Id == Id);
            _context.Products.Remove(product);
        }

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="Id">Takes id parameter</param>
        /// <returns>Return one object by id.</returns>
        public Product GetEntity(int Id)
        {
            return _context.Products.Find(Id);
        }

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public PagedList<Product> GetList(PageParameters pageParameters)
        {
            return PagedList<Product>.ToPagedList(_context.Products,
                pageParameters.PageNumber,
                pageParameters.PageSize);
        }

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public IList<Product> GetList()
        {
            return _context.Products.ToList();
        }

        /// <summary>
        /// Update method.
        /// Updates an object of Product class.
        /// </summary>
        /// <param name="product">Takes an object of Product class.</param>
        public void Update(Product product)
        {
            var entity = _context.Products.Find(product.Id);

            entity.Price = product.Price;
            entity.ProductName = product.ProductName;
            entity.UnitOfMeasurement = product.UnitOfMeasurement;

            _context.Products.Update(entity);
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
