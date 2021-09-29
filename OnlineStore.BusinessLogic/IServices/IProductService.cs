using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.DataModel;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.IServices
{

    /// <summary>
    /// ProductService interface.
    /// </summary>
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts();
        void CreateProduct(ProductDto product);
        void UpdateProduct(ProductDto product);
        ProductDto FindProductById(int id);
        void DeleteProduct(ProductDto product);
    }
}
