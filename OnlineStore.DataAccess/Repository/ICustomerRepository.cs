using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.RepositoryPatterns
{
    interface ICustomerRepository : IDisposable, IRepository<Customer>
    {
  
    }
}
