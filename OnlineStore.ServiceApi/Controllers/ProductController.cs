using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using System.Collections.Generic;

namespace OnlineStore.ServiceApi.Controllers
{
    /// <summary>
    /// ServiceApi ProductController.
    /// </summary>
    [Route("serviceApi/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Product service field.
        /// </summary>
        private IProductService _productService;

        /// <summary>
        /// PoductController constructor.
        /// </summary>
        /// <param name="productService">Product service</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// HttpGet endpoint with all products.
        /// </summary>
        /// <returns>List with all products</returns>
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

        /// <summary>
        /// HttpGet endpoint with all products names.
        /// </summary>
        /// <returns>List with all products names</returns>
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

        /// <summary>
        /// HttpGet endpoint with product by id.
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product</returns>
        [HttpGet]
        [Route("getProduct/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ProductDto> GetProductById(int id)
        {
            var product = _productService.FindProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// HttpPost endpoint. Creates product.
        /// </summary>
        /// <param name="product">Product</param>
        [HttpPost]
        [Route("createProduct")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ProductDto> CreateProduct(ProductDto product)
        {
            _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { Id = product.Id }, product);
        }

        /// <summary>
        /// HttpPut endpoint. Updates product.
        /// </summary>
        /// <param name="productService">product</param>
        [HttpPut]
        [Route("updateProduct")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateProduct([FromBody]ProductDto productService)
        {
            var product = _productService.FindProductById(productService.Id);

            if (product == null)
            {
                return NotFound();
            }

            product.Price = productService.Price;
            product.ProductName = productService.ProductName;
            product.UnitOfMeasurement = productService.UnitOfMeasurement;

            _productService.UpdateProduct(product);

            return NoContent();
        }

        /// <summary>
        /// HttpDelete endpoint. Deletes product.
        /// </summary>
        /// <param name="id">Product id</param>
        [HttpDelete]
        [Route("deleteProduct/{id}")]
        [Authorize(Roles = "Admin")]
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
