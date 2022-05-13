using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DataAccess.RepositoryPatterns;

namespace OnlineStore.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public ISaleRepository Sales { get; }
        public ICustomerRepository Customers { get; }
        public IProductRepository Products { get; }

        public void Save();

        public void BeginTransaction();

        public void Commit();

        public void Rollback();
    }
}
