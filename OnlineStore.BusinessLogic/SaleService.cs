using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;
using System.Linq;

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
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// SaleService constructor.
        /// </summary>
        /// <param name="sale">Sale repository</param>
        public SaleService(ISaleRepository sale, IMapper mapper)
        {
            _sale = sale;
            _mapper = mapper;
        }

        /// <summary>
        /// GetAllSales method.
        /// </summary>
        /// <returns>All sale objects from table</returns>
        public IEnumerable<SaleDto> GetAllSales()
        {
            var sales = _sale
                .GetList()
                .Select(s => new SaleDto
                {
                    Id = s.Id,
                    CustomerId = s.CustomerId,
                    ProductId = s.ProductId,
                    CustomerName = $"{s.Customer.FirstName.Substring(0, 1)}. {s.Customer.LastName}",
                    ProductName = s.Product.ProductName,
                    DateOfSale = s.DateOfSale,
                    Amount = s.Amount
                }) ;
            
            return _mapper.Map<IEnumerable<SaleDto>>(sales);
        }

        /// <summary>
        /// CreateSale method.
        /// </summary>
        /// <param name="saleModel">Takes saleDto object</param>
        public void CreateSale(SaleDto saleModel)
        {
            var sale = _mapper.Map<Sale>(saleModel);
            _sale.Create(sale);
            _sale.Save();
        }

        /// <summary>
        /// UpdateSale method.
        /// </summary>
        /// <param name="saleModel">Takes saleDto object</param>
        public void UpdateSale(SaleDto saleModel)
        {
            var sale = _mapper.Map<Sale>(saleModel);
            _sale.Update(sale);
            _sale.Save();
        }

        /// <summary>
        /// FindSaleById method.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>SaleDto object.</returns>
        public SaleDto FindSaleById(int id)
        {
            var sale = _sale.GetEntity(id);
            var saleDto = _mapper.Map<SaleDto>(sale);
            saleDto.ProductName = sale.Product.ProductName;
            saleDto.CustomerName = $"{sale.Customer.FirstName.Substring(0, 1)}." +
                $" {sale.Customer.LastName}";

            return saleDto;
        }

        /// <summary>
        /// DeleteSale method.
        /// </summary>
        /// <param name="id">id</param>
        public void DeleteSale(int id)
        {
            _sale.Delete(id);
            _sale.Save();
        }

    }
}
