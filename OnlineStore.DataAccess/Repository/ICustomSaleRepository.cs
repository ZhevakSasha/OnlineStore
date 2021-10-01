using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;

namespace OnlineStore.DataAccess.Repository
{
    interface ICustomSaleRepository : ISaleRepository
    {
        IEnumerable<Sale> GetCustomSalesList();
    }
}
