using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.Domain.Models;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.IServices
{
    /// <summary>
    /// ProductService interface.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// GetAllProducts method.
        /// </summary>
        /// <returns>Returns all products from table</returns>
        PagedList<ProductDto> GetAllProducts(PageParameters pageParameters);

        /// <summary>
        /// CreateProduct method. 
        /// </summary>
        /// <param name="product">Takes ProductDto object</param>
        void CreateProduct(ProductDto product);

        /// <summary>
        /// Update product method.
        /// </summary>
        /// <param name="product">Takes ProductDto object</param>
        void UpdateProduct(ProductDto product);

        /// <summary>
        /// FindProductById method.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ProductDto object</returns>
        ProductDto FindProductById(int id);

        /// <summary>
        /// DeleteProduct method deletes product by id.
        /// </summary>
        /// <param name="id">id</param>
        void DeleteProduct(int id);

        /// <summary>
        /// GetAllProductNames method.
        /// </summary>
        /// <returns>Returns all product names from cusomer table</returns>
        IEnumerable<SelectDto> GetAllProductNames(PageParameters pageParameters);
    }
}
