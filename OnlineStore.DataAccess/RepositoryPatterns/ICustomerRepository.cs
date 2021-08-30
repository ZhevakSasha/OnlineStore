using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.RepositoryPatterns
{
    interface ICustomerRepository : IDisposable
    {
        IEnumerable<Customer> GetCustomerList();
        Customer GetCustomer(int Id);
        void Create(Customer item);
        void Update(Customer item);

        void Delete(int Id);

        void Save();

    }
}
