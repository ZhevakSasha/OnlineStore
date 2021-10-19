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
    /// <summary>
    /// Product service tests.
    /// </summary>
    public class ProductServiceTests
    {
        /// <summary>
        /// Product service object.
        /// </summary>
        private IProductService _productService;

        /// <summary>
        /// Mock product repository object.
        /// </summary>
        private Mock<IProductRepository> _mockRepository;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Setup method.
        /// </summary>
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

        /// <summary>
        /// Testing GetAllProducts method.
        /// </summary>
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

        /// <summary>
        /// Fake GetList method.
        /// </summary>
        /// <returns>Products list</returns>
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

        /// <summary>
        /// Testing GetProductById method.
        /// </summary>
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
        /// <summary>
        /// Testing CreateProduct method.
        /// </summary>

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

        /// <summary>
        /// Testing DeleteProduct method.
        /// </summary>
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

        /// <summary>
        /// Testing UpdateProduct method.
        /// </summary>
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