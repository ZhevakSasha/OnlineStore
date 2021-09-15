﻿using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System;
using System.Collections.Generic;

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
        private readonly DataBaseContext context;

        /// <summary>
        /// Constructor for private string field connectionString.
        /// </summary>
        /// <param name="connectionString"></param>
        public EntityFrameworkProductRepository(string connectionString)
        {
            context = new DataBaseContext(connectionString);
        }

        /// <summary>
        /// Adds an object of Product class in the database.
        /// </summary>
        /// <param name="product"></param>
        public void Create(Product product)
        {
            context.Products.Add(product);
        }

        /// <summary>
        /// Deletes an object of Product class.
        /// </summary>
        /// <param name="product"></param>
        public void Delete(Product product)
        {
            if (product != null)
                context.Products.Remove(product);
        }

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="Id">Takes id parameter</param>
        /// <returns>Return one object by id.</returns>
        public Product GetEntity(int Id)
        {
            return context.Products.Find(Id);
        }

        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        public IEnumerable<Product> GetList()
        {
            return context.Products;
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
        /// Updates an object of Product class.
        /// </summary>
        /// <param name="product">Takes an object of Product class.</param>
        public void Update(Product product)
        {
            context.Products.Update(product);
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
