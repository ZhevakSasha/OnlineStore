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
        public ProductController(IProductService product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        public IActionResult ProductTable()
        {
            var results = _product.GetAllProducts();
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(results);
            return View(products);
        }

        public IActionResult ProductUpdating(int id)
        {
            var product = _product.FindProductById(id);
            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        public IActionResult ProductUpdating(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _product.UpdateProduct(_mapper.Map<ProductDto>(product));
                return RedirectToAction("ProductTable");
            }
            else
                return View();
        }

        public IActionResult ProductCreating()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductCreating(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _product.CreateProduct(_mapper.Map<ProductDto>(product));
                return RedirectToAction("ProductTable");
            }
            else
                return View();
        }

        public IActionResult ProductDeleting(int id)
        {
            var product = _product.FindProductById(id);
            _product.DeleteProduct(id);
            return RedirectToAction("ProductTable");
        }
    }
}
