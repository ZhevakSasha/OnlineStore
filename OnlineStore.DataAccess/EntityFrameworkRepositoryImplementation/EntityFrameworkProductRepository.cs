﻿using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
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
            var product = _context.Products.Local.First(c => c.Id == Id);
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
        public IEnumerable<Product> GetList()
        {
            return _context.Products;
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
        /// Updates an object of Product class.
        /// </summary>
        /// <param name="product">Takes an object of Product class.</param>
        public void Update(Product product)
        {
            _context.Products.Update(product);
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
