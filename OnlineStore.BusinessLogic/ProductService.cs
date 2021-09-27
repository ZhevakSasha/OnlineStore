using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;


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
        /// ProductService constructor.
        /// </summary>
        /// <param name="product">Product repository</param>
        public ProductService(IProductRepository product)
        {
            _product = product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _product.GetList();
        }

        public void CreateProduct(Product product)
        {
            _product.Create(product);
            _product.Save();
        }

        public void UpdateProduct(Product product)
        {
            _product.Update(product);
            _product.Save();
        }

        public Product FindProductById(int id)
        {
            return _product.GetEntity(id);
        }

        public void DeleteProduct(Product product)
        {
            _product.Delete(product);
            _product.Save();
        }
    }
}
