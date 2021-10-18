using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineStore.BusinessLogic;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using OnlineStore.MvcApplication;

namespace OnlineStore.DataAccess.Tests.ServiceTests
{
    public class ProductServiceTests
    {
        private IProductService _productService;

        private Mock<IProductRepository> _mockRepository;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IProductRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _productService = new ProductService(_mockRepository.Object, _mapper);
        }

        [Test]
        public void GetAllProducts_ReturnsAIEnumerableProductsDto()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetList()).Returns(GetTestProducts());

            // Act
            var result = _productService.GetAllProducts();

            // Assert
            Assert.AreEqual(GetTestProducts().Count(), result.Count());

        }

        private List<Product> GetTestProducts()
        {
            var products = new List<Product>
            {
                new Product() { Id=1, ProductName="c1", Price = 100, UnitOfMeasurement="pc."},
                new Product() { Id=2, ProductName="c2", Price = 200, UnitOfMeasurement="pc."},
                new Product() { Id=3, ProductName="c3", Price = 300, UnitOfMeasurement="pc."}
            };
            return products;
        }

        [Test]
        public void GetProductById_ReturnsProductDtoById()
        {
            // Arrange
            const int arbitraryId = 1;
            var expected = new Product()
            {
                Id = arbitraryId,
                ProductName = "c1",
                Price = 100,
                UnitOfMeasurement = "pc."
            };
            _mockRepository.Setup(repo => repo.GetEntity(arbitraryId)).Returns(expected);

            // Act
            var result = _productService.FindProductById(arbitraryId);

            // Assert
            result.Should().BeEquivalentTo(_mapper.Map<ProductDto>(expected));
        }

        [Test]
        public void CreateProduct_ChecksTheCallOfCreatingMethod()
        {
            // Arrange
            const int arbitraryId = 1;
            var expected = new Product()
            {
                Id = arbitraryId,
                ProductName = "c1",
                Price = 100,
                UnitOfMeasurement = "pc."
            };
            _mockRepository.Setup(repo => repo.Create(It.IsAny<Product>())).Verifiable();

            // Act
            _productService.CreateProduct(_mapper.Map<ProductDto>(expected));

            // Assert
            _mockRepository.Verify(repo => repo.Create(It.IsAny<Product>()), Times.Once);

        }

        [Test]
        public void DeleteProduct_ChecksTheCallOfDeletingMethod()
        {
            // Arrange
            const int arbitraryId = 1;
            _mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Verifiable();

            // Act
            _productService.DeleteProduct(arbitraryId);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void UpdateProduct_ChecksTheCallOfUpdateMethod()
        {
            // Arrange
            const int arbitraryId = 1;
            var expected = new Product()
            {
                Id = arbitraryId,
                ProductName = "c1",
                Price = 100,
                UnitOfMeasurement = "pc."
            };
            _mockRepository.Setup(repo => repo.Update(It.IsAny<Product>())).Verifiable();

            // Act
            _productService.UpdateProduct(_mapper.Map<ProductDto>(expected));

            // Assert
            _mockRepository.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);

        }
    }
}