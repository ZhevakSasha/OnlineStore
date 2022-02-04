using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using OnlineStore.DataAccess;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.Domain.Models;

namespace OnlineStore.BusinessLogic
{
    /// <summary>
    /// Product service.
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Product repository.
        /// </summary>
        private UnitOfWork _unitOfWork;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// ProductService constructor.
        /// </summary>
        /// <param name="product">Product repository</param>
        public ProductService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// GetAllProducts method
        /// </summary>
        /// <returns>All product objects from table</returns>
        public PagedList<ProductDto> GetAllProducts(PageParameters pageParameters)
        {
            var products = _unitOfWork.Products.GetList(pageParameters);
            var count = products.TotalCount;

            return new PagedList<ProductDto>(_mapper.Map<List<ProductDto>>(products),
                count,
                pageParameters.PageNumber,
                pageParameters.PageSize);
        }

        /// <summary>
        /// CreateProduct method.
        /// </summary>
        /// <param name="productModel">Takes productDto object</param>
        public void CreateProduct(ProductDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            _unitOfWork.Products.Create(product);
            _unitOfWork.Save();
        }

        /// <summary>
        /// UpdateProduct method.
        /// </summary>
        /// <param name="productModel">Takes productDto object</param>
        public void UpdateProduct(ProductDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
        }

        /// <summary>
        /// FindProductById method.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ProductDto object</returns>
        public ProductDto FindProductById(int id)
        {
            var product = _unitOfWork.Products.GetEntity(id);
            return _mapper.Map<ProductDto>(product);
        }

        /// <summary>
        /// GetAllProductNames method.
        /// </summary>
        /// <returns>IEnumerable<SelectDto> product names</returns>
        public IList<SelectDto> GetAllProductNames()
        {
            var productNames = _unitOfWork.Products
                .GetList()
                .Select(s => new SelectDto
                {
                    Id = s.Id,
                    Name = s.ProductName
                }).ToList();
            return productNames;
        }

        /// <summary>
        /// DeleteProduct method.
        /// </summary>
        /// <param name="id">id</param>
        public void DeleteProduct(int id)
        {
            _unitOfWork.Products.Delete(id);
            _unitOfWork.Save();
        }
    }
}
