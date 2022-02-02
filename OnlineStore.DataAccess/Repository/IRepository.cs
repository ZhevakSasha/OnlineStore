using OnlineStore.DataAccess.PagedList;
using OnlineStore.Domain.Models;
using System.Collections.Generic;

namespace OnlineStore.DataAccess.RepositoryPatterns
{
    /// <summary>
    /// General interface repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> 
        where T : class
    {
        /// <summary>
        /// GetList method. 
        /// </summary>
        /// <returns>Returns all objects.</returns>
        PagedList<T> GetList(PageParameters pageParameters);

        /// <summary>
        /// GetEntity method.
        /// </summary>
        /// <param name="id">Takes id parameter. </param>
        /// <returns>Return one object by id. </returns>
        T GetEntity(int Id);

        /// <summary>
        /// Create method.
        /// Creates an object of Product class.
        /// </summary>
        /// <param name="product">Takes an object of Product class.</param>
        void Create(T item);

        /// <summary>
        /// Update method.
        /// Updates an object of Product class.
        /// </summary>
        /// <param name="product">Takes an object of Product class.</param>
        void Update(T item);

        /// <summary>
        /// Delete method.
        /// Deletes an object of Product class.
        /// </summary>
        /// <param name="product">Takes an object of Product class.</param>
        void Delete(int Id);

    }
}
