using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ServiceApi.Controllers
{
    [Route("serviceApi/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getProducts")]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getProductsNames")]
        public ActionResult<IEnumerable<SelectDto>> GetAllProductsNames()
        {
            var productsNames = _productService.GetAllProductNames();

            if (productsNames == null)
            {
                return NotFound();
            }

            return Ok(productsNames);
        }

        [HttpGet("id", Name = "getProduct")]
        public ActionResult<ProductDto> GetProductById(int id)
        {
            var product = _productService.FindProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("createProduct")]
        public ActionResult<ProductDto> CreateProduct(ProductDto product)
        {
            _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { Id = product.Id }, product);
        }

        [HttpPut]
        [Route("updateProduct")]
        public ActionResult UpdateProduct(ProductDto productService)
        {
            var product = _productService.FindProductById(productService.Id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.UpdateProduct(product);

            return NoContent();
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _productService.FindProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);

            return NoContent();
        }
    }
}
