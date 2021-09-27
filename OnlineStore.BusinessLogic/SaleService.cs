using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic
{
    public class SaleService : ISaleService
    {
        private ISaleRepository _sale;

        public SaleService(ISaleRepository sale)
        {
            _sale = sale;
        }

        public IEnumerable<Sale> GetAllSales()
        {
            return _sale.GetList();
        }

        public void CreateSale(Sale sale)
        {
            _sale.Create(sale);
            _sale.Save();
        }

        public void UpdateSale(Sale sale)
        {
            _sale.Update(sale);
            _sale.Save();
        }

        public Sale FindSaleById(int id)
        {
            return _sale.GetEntity(id);
        }

        public void DeleteSale(Sale sale)
        {
            _sale.Delete(sale);
            _sale.Save();
        }
    }
}
