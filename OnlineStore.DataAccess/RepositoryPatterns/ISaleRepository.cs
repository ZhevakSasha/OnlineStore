using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.RepositoryPatterns
{
    interface ISaleRepository
    {
        IEnumerable<Sale> GetCustomerList();
        Sale GetCustomer(int Id);
        void Create(Sale item);
        void Update(Sale item);
        void Delete(int Id);
        void Save();
    }
}
