using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
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

        private IMapper _mapper;

        /// <summary>
        /// SaleService constructor.
        /// </summary>
        /// <param name="sale">Sale repository</param>
        public SaleService(ISaleRepository sale, IMapper mapper)
        {
            _sale = sale;
        }

        public IEnumerable<SaleDto> GetAllSales()
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

        public void DeleteSale(int id)
        {
            _sale.Delete(id);
            _sale.Save();
        }
    }
}
