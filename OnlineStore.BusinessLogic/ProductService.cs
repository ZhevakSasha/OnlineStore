using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;
using AutoMapper;


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

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var products = _product.GetList();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public void CreateProduct(ProductDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            _product.Create(product);
            _product.Save();
        }

        public void UpdateProduct(ProductDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            _product.Update(product);
            _product.Save();
        }

        public ProductDto FindProductById(int id)
        {
            var product = _product.GetEntity(id);
            return _mapper.Map<ProductDto>(product);
        }

        public void DeleteProduct(ProductDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            _product.Delete(product);
            _product.Save();
        }
    }
}
