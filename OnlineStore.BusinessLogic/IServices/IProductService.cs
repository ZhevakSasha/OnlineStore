using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.BusinessLogic.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        Product FindProductById(int id);
        void DeleteProduct(Product product);
    }
}
