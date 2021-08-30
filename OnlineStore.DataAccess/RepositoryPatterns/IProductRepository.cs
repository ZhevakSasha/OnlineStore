using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.RepositoryPatterns
{
    interface IProductRepository
    {
        IEnumerable<Product> GetCustomerList();
        Product GetCustomer(int Id);
        void Create(Product item);
        void Update(Product item);
        void Delete(int Id);
        void Save();
    }
}
