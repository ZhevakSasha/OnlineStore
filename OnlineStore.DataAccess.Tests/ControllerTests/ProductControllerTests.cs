using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.MvcApplication.Controllers;
using OnlineStore.BusinessLogic.DtoModels;
using AutoMapper;
using Xunit;
using System.Linq;
using OnlineStore.MvcApplication;
using OnlineStore.MvcApplication.Models;

namespace OnlineStore.DataAccess.Tests.ControllerTests
{
    public class ProductControllerTests
    {

        private Mock<IProductService> _mockService;

        private ProductController _productController;

        private IMapper _mapper;

        public ProductControllerTests()
        {
            _mockService = new Mock<IProductService>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();

            _productController = new ProductController(_mockService.Object, _mapper);
        }

        [Fact]
        public void ProductTableReturnsAViewResultWithAListOfProducts()
        {
            // Arrange
            _mockService.Setup(repo => repo.GetAllProducts()).Returns(GetTestProducts());

            // Act
            var result = _productController.ProductTable();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(viewResult.Model);
            Assert.Equal(GetTestProducts().Count(), model.Count());
        }

        private List<ProductDto> GetTestProducts()
        {
            var products = new List<ProductDto>
            {
                new ProductDto() { Id=1, ProductName="c1", Price = 100, UnitOfMeasurement="pc."},
                new ProductDto() { Id=2, ProductName="c2", Price = 200, UnitOfMeasurement="pc."},
                new ProductDto() { Id=3, ProductName="c3", Price = 300, UnitOfMeasurement="pc."}
            };
            return products;
        }

        [Fact]
        public void AddProductReturnsViewResultWithProductModel()
        {
            // Arrange

            _productController.ModelState.AddModelError("ProductName", "Required");
            ProductViewModel newProduct = new ProductViewModel();

            // Act
            var result = _productController.ProductCreating(newProduct);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(It.IsAny<ProductViewModel>(), viewResult?.Model);
        }

        [Fact]
        public void AddCutomerReturnsARedirectAndAddsProduct()
        {
            // Arrange
            var newProduct = new ProductViewModel() { Id = 1, ProductName = "c1", Price = 100, UnitOfMeasurement = "pc." };
            _mockService.Setup(r => r.CreateProduct(It.IsAny<ProductDto>())).Verifiable();

            // Act
            var result = _productController.ProductCreating(newProduct);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ProductTable", redirectToActionResult.ActionName);
            _mockService.Verify(r => r.CreateProduct(It.IsAny<ProductDto>()));
        }

        [Fact]
        public void UpdateCutomerReturnsARedirectAndUpdatesProduct()
        {
            // Arrange
            var newProduct = new ProductViewModel() { Id = 1, ProductName = "c1", Price = 100, UnitOfMeasurement = "pc." };
            _mockService.Setup(r => r.UpdateProduct(It.IsAny<ProductDto>())).Verifiable();

            // Act
            var result = _productController.ProductUpdating(newProduct);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ProductTable", redirectToActionResult.ActionName);
            _mockService.Verify(r => r.UpdateProduct(It.IsAny<ProductDto>()));
        }

        [Fact]
        public void UpdateProductReturnsViewResultWithProductModel()
        {
            // Arrange
            _productController.ModelState.AddModelError("ProductName", "Required");
            ProductViewModel newProduct = new ProductViewModel();

            // Act
            var result = _productController.ProductUpdating(newProduct);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(It.IsAny<ProductViewModel>(), viewResult?.Model);
        }

        [Fact]
        public void DeleteCutomerReturnsARedirectAndDeletesProduct()
        {
            // Arrange
            var newProduct = new ProductViewModel();
            _mockService.Setup(r => r.DeleteProduct(It.IsAny<int>())).Verifiable();

            // Act
            var result = _productController.ProductDeleting(newProduct);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ProductTable", redirectToActionResult.ActionName);
            _mockService.Verify(r => r.DeleteProduct(It.IsAny<int>()));
        }
    }
}