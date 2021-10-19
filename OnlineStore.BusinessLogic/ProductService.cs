using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;

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
        private IProductRepository _product;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// ProductService constructor.
        /// </summary>
        /// <param name="product">Product repository</param>
        public ProductService(IProductRepository product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        /// <summary>
        /// GetAllProducts method
        /// </summary>
        /// <returns>All product objects from table</returns>
        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = _product.GetList();         
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        /// <summary>
        /// CreateProduct method.
        /// </summary>
        /// <param name="productModel">Takes productDto object</param>
        public void CreateProduct(ProductDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            _product.Create(product);
            _product.Save();
        }

        /// <summary>
        /// UpdateProduct method.
        /// </summary>
        /// <param name="productModel">Takes productDto object</param>
        public void UpdateProduct(ProductDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            _product.Update(product);
            _product.Save();
        }

        /// <summary>
        /// FindProductById method.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ProductDto object</returns>
        public ProductDto FindProductById(int id)
        {
            var product = _product.GetEntity(id);
            return _mapper.Map<ProductDto>(product);
        }

        /// <summary>
        /// GetAllProductNames method.
        /// </summary>
        /// <returns>IEnumerable<SelectDto> product names</returns>
        public IEnumerable<SelectDto> GetAllProductNames()
        {
            var productNames = _product
                .GetList()
                .Select(s => new SelectDto
                {
                    Id = s.Id,
                    Name = s.ProductName
                }) ;
            return productNames;
        }

        /// <summary>
        /// DeleteProduct method.
        /// </summary>
        /// <param name="id">id</param>
        public void DeleteProduct(int id)
        {
            _product.Delete(id);
            _product.Save();  
        }
    }
}
