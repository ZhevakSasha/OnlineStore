using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic
{
    /// <summary>
    /// Sale service.
    /// </summary>
    public class SaleService : ISaleService
    {

        /// <summary>
        /// Sale repository.
        /// </summary>
        private ISaleRepository _sale;

        /// <summary>
        /// SaleService constructor.
        /// </summary>
        /// <param name="sale">Sale repository</param>
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
