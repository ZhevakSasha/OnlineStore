using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic;
using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Controllers
{
    public class ProductController : Controller
    {
        private ProductLogic _product;

        private DataBaseContext _context;

        public ProductController(DataBaseContext context)
        {
            _context = context;
            _product = new ProductLogic(_context);
        }
        public IActionResult ProductTable()
        {
            var results = _product.GetAllProducts();
            return View(results);
        }

        public IActionResult ProductUpdating(int id)
        {
            Product product = _product.FindProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult ProductUpdating(Product product)
        {
            _product.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        public IActionResult ProductCreating()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductCreating(Product product)
        {
            if (ModelState.IsValid)
            {
                _product.CreateProduct(product);
                return RedirectToAction("ProductTable");
            }
            else
                return View();
        }

        public IActionResult ProductDeleting(int id)
        {
            Product product = _product.FindProductById(id);
            _product.DeleteProduct(product);
            return RedirectToAction("ProductTable");
        }
    }
}
