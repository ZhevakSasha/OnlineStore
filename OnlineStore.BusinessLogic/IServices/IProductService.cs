using OnlineStore.DataAccess.DataModel;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.IServices
{

    /// <summary>
    /// ProductService interface.
    /// </summary>
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        Product FindProductById(int id);
        void DeleteProduct(Product product);
    }
}
