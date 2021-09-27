using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.RepositoryPatterns
{
    /// <summary>
    /// Product repository interface. 
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
     
    }
}
