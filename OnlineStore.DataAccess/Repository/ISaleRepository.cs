using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.DataAccess.RepositoryPatterns
{
    interface ISaleRepository : IDisposable, IRepository<Sale>
    {
        
    }
}
