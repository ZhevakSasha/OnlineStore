using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.Exceptions;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.Domain.Models;
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
        private UnitOfWork _unitOfWork;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// SaleService constructor.
        /// </summary>
        /// <param name="sale">Sale repository</param>
        public SaleService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// GetAllSales method.
        /// </summary>
        /// <returns>All sale objects from table</returns>
        public PagedList<SaleDto> GetAllSales(PageParameters pageParameters)
        {
            var sales = _unitOfWork.Sales
                .GetList(pageParameters);
            var count = sales.TotalCount;

            return new PagedList<SaleDto>(_mapper.Map<List<SaleDto>>(sales),
                count,
                pageParameters.PageNumber,
                pageParameters.PageSize);
        }

        /// <summary>
        /// CreateSale method.
        /// </summary>
        /// <param name="saleModel">Takes saleDto object</param>
        public void CreateSale(SaleDto saleModel)
        {
            var sale = _mapper.Map<Sale>(saleModel);
            var newProducts = new List<Product>();
            if (_unitOfWork.Customers.GetEntity(sale.CustomerId) == null) throw new BLException($"Customer {sale.CustomerId} is not found");
            foreach(var product in sale.Products)
            {
                if (_unitOfWork.Products.GetEntity(product.Id) == null) throw new BLException($"Product {product.Id} is not found");
                newProducts.Add(_unitOfWork.Products.GetEntity(product.Id));
            }

            sale.Products = newProducts;
            _unitOfWork.Sales.Create(sale);
            _unitOfWork.Save();
        }

        public void CreateSaleWithProduct(SaleWithProductDto saleWithProduct)
        {
            //var productDto = new ProductDto
            //{
            //    Price = saleWithProduct.Price,
            //    ProductName = saleWithProduct.ProductName.Last(),
            //    UnitOfMeasurement = saleWithProduct.UnitOfMeasurement
            //};

            //var product = _mapper.Map<Product>(productDto);

            //_unitOfWork.Products.Create(product);

            _unitOfWork.Save();

            var saleDto = new SaleDto
            {
                Amount = saleWithProduct.Amount,
                //Product = saleWithProduct.ProductName,
                CustomerId = saleWithProduct.CustomerId,
                CustomerName = saleWithProduct.CustomerName,
                DateOfSale = saleWithProduct.DateOfSale
            };

            var sale = _mapper.Map<Sale>(saleDto);

            _unitOfWork.Sales.Create(sale);

            _unitOfWork.Save();
        }

        /// <summary>
        /// UpdateSale method.
        /// </summary>
        /// <param name="saleModel">Takes saleDto object</param>
        public void UpdateSale(SaleDto saleModel)
        {
            var sale = _mapper.Map<Sale>(saleModel);
            var newProducts = new List<Product>();
            foreach (var product in sale.Products)
            {
                newProducts.Add(_unitOfWork.Products.GetEntity(product.Id));
            }
            sale.Products = newProducts;
            _unitOfWork.Sales.Update(sale);
            _unitOfWork.Save();
        }

        /// <summary>
        /// FindSaleById method.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>SaleDto object.</returns>
        public SaleDto FindSaleById(int id)
        {
            var sale = _unitOfWork.Sales.GetEntity(id);
            var saleDto = _mapper.Map<SaleDto>(sale);

            return saleDto;
        }

        /// <summary>
        /// DeleteSale method.
        /// </summary>
        /// <param name="id">id</param>
        public void DeleteSale(int id)
        {
            _unitOfWork.Sales.Delete(id);
            _unitOfWork.Save();
        }
    }
}
