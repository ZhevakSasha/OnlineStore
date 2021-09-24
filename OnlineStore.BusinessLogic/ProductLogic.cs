using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.BusinessLogic
{
    public class ProductLogic
    {
        /// <summary>
        /// Context field.
        /// </summary>
        private readonly DataBaseContext _context;

        private EntityFrameworkProductRepository _product;

        public ProductLogic(DataBaseContext context)
        {
            _context = context;
            _product = new EntityFrameworkProductRepository(_context);
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
