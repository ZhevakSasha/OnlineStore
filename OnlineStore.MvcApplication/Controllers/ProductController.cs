using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.MvcApplication.Models;
using System.Collections.Generic;

namespace OnlineStore.MvcApplication.Controllers
{
    /// <summary>
    /// Product controller.
    /// </summary>
    public class ProductController : Controller
    {

        /// <summary>
        /// Product service.
        /// </summary>
        private IProductService _product;

        /// <summary>
        /// ProductController constructor.
        /// </summary>
        /// <param name="product">Product service</param>
        public ProductController(IProductService product)
        {
            _product = product;
        }
        public IActionResult ProductTable()
        {
            var results = _product.GetAllProducts();
            return View(results);
        }

        public IActionResult ProductUpdating(int id)
        {
            ProductDto product = _product.FindProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult ProductUpdating(ProductDto product)
        {
            _product.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        public IActionResult ProductCreating()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductCreating(ProductDto product)
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
            ProductDto product = _product.FindProductById(id);
            _product.DeleteProduct(product);
            return RedirectToAction("ProductTable");
        }
    }
}
