using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.RepositoryPatterns
{
    /// <summary>
    /// General interface repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetList();
        T GetEntity(int Id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        void Save();
    }
}
