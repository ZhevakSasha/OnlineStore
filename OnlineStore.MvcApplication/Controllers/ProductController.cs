using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
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
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// ProductController constructor.
        /// </summary>
        /// <param name="product">Product service</param>
        /// <param name="mapper">Mapper</param>
        public ProductController(IProductService product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        /// <summary>
        /// Takes a list of all products from the table and passes them into view.
        /// </summary>
        /// <returns>View with products</returns>
        public IActionResult ProductTable()
        {
            var results = _product.GetAllProducts();
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(results);
            return View(products);
        }

        /// <summary>
        /// Takes product data by id from the table and passes them into view.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>View with product</returns>
        public IActionResult ProductUpdating(int id)
        {
            var product = _product.FindProductById(id);
            return View(_mapper.Map<ProductViewModel>(product));
        }

        /// <summary>
        /// Updates product data.
        /// </summary>
        /// <param name="product">Takes productViewModel object</param>
        /// <returns>ProductTable view</returns>
        [HttpPost]
        public IActionResult ProductUpdating(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _product.UpdateProduct(_mapper.Map<ProductDto>(product));
                return RedirectToAction("ProductTable");
            }
            return View();
        }

        /// <summary>
        /// ProductCreating.
        /// </summary>
        /// <returns>ProductCreating View</returns>
        public IActionResult ProductCreating()
        {
            return View();
        }

        /// <summary>
        /// Saves product data.
        /// </summary>
        /// <param name="product">Takes productViewModel object</param>
        /// <returns>Table view</returns>
        [HttpPost]
        public IActionResult ProductCreating(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _product.CreateProduct(_mapper.Map<ProductDto>(product));
                return RedirectToAction("ProductTable");
            }
            return View();
        }

        /// <summary>
        /// Removes product.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ProductTable view</returns>
        public IActionResult ProductDeleting(int id)
        {
            var product = _mapper.Map<ProductViewModel>(_product.FindProductById(id));
            return View(product);
        }

        /// <summary>
        /// Removes product.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ProductTable view</returns>
        [HttpPost]
        public IActionResult ProductDeleting(ProductViewModel product)
        {
            
            _product.DeleteProduct(product.Id);
            return RedirectToAction("ProductTable");
        }
    }
}
