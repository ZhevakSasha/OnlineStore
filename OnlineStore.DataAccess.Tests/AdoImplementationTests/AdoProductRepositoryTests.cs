using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;
using OnlineStore.DataAccess.DataModel;
using System.Collections.Generic;

namespace OnlineStore.DataAccess.Tests
{
    /// <summary>
    /// AdoProductRepository tests class.
    /// </summary>
    class AdoProductRepositoryTests
    {
        /// <summary>
        /// DataBaseConfiguration object.
        /// </summary>
        private DataBaseConfiguration _dbConfiguration;

        /// <summary>
        /// AdoProductRepository object.
        /// </summary>
        private AdoProductRepository _product;

        /// <summary>
        /// IConfiguration field.
        /// </summary>
        private IConfiguration _configuration;

        /// <summary>
        /// Setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
             .AddJsonFile(path: "appconfig.json")
             .Build();

            _dbConfiguration = new DataBaseConfiguration(_configuration);
            _dbConfiguration.DeployTestDatabase();

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            _product = new AdoProductRepository(connectionString);
        }

        /// <summary>
        /// Testing GetEntity method.
        /// </summary>
        [Test]
        public void Get_WhenTakesProductId_ThenReturnsProduct()
        {
            //Arrange
            const int concreteId = 1;
            var expected = new Product()
            {
                Id = concreteId,
                ProductName = "Keyboard",
                Price = 200,
                UnitOfMeasurement = "pc."
            };

            //Act
            var actual = _product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Create method.
        /// </summary>
        [Test]
        public void Create_WhenTakesProduct_ThenCreateProduct()
        {
            //Arrange
            const int concreteId = 3;
            var expected = new Product()
            {
                Id = concreteId,
                ProductName = "Phone",
                Price = 180,
                UnitOfMeasurement = "pc."
            };

            //Act
            _product.Create(expected);
            var actual = _product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhenTakesProduct_ThenDeleteProduct()
        {
            //Arrange
            const int concreteId = 2;
            var arbitraryProduct = new Product()
            {
                Id = concreteId,
                ProductName = "Mouse",
                Price = 120,
                UnitOfMeasurement = "pc."
            };
            //Сreating an empty object. 
            var expected = new Product();

            //Act
            _product.Delete(concreteId);
            var actual = _product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenTakesProduct_ThenUpdateProduct()
        {
            //Arrange
            const int concreteId = 2;
            var arbitraryUpdatedProduct = new Product()
            {
                Id = concreteId,
                ProductName = "Phone2",
                Price = 190,
                UnitOfMeasurement = "pc."
            };
            var expected = arbitraryUpdatedProduct;

            //Act
            _product.Update(arbitraryUpdatedProduct);
            var actual = _product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing GetList method.
        /// </summary>
        [Test]
        public void Get_ReturnProductList()
        {
            //Arrange
            var expected = new List<Product>()
            {
                new Product()
                {
                Id = 1,
                ProductName = "Keyboard",
                Price = 200,
                UnitOfMeasurement = "pc."
                },
                new Product()
                {
                Id = 2,
                ProductName = "Mouse",
                Price = 120,
                UnitOfMeasurement = "pc."
                }
            };

            //Act
            var actual = _product.GetList();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
