using NUnit.Framework;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using System.Collections.Generic;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;

namespace OnlineStore.DataAccess.Tests.EntityFrameworkImplementationTests
{
    class EntityFrameworkProductRepositoryTests
    {
         /// <summary>
        /// DataBaseConfiguration object.
        /// </summary>
        private DataBaseConfiguration _dbConfiguration;

        /// <summary>
        /// EntityFrameworkProductRepository object.
        /// </summary>
        private EntityFrameworkProductRepository Product;

        /// <summary>
        /// IConfiguration field.
        /// </summary>
        public IConfiguration Configuration;


        /// <summary>
        /// Setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Configuration = new ConfigurationBuilder()
             .AddJsonFile(path: "appconfig.json")
             .Build();

            _dbConfiguration = new DataBaseConfiguration(Configuration);
            _dbConfiguration.DeployTestDatabase();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            Product = new EntityFrameworkProductRepository(connectionString);
        }

        /// <summary>
        /// Testing GetEntity method.
        /// </summary>
        [Test]
        public void Get_WhereProductById_ThenReturnsProduct()
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
            var actual = Product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Create method.
        /// </summary>
        [Test]
        public void Create_WhenProduct_ThenCreateProduct()
        {
            //Arrange
            const int concreteId = 3;
            var expected = new Product()
            {
                ProductName = "Phone",
                Price = 180,
                UnitOfMeasurement = "pc."
            };

            //Act
            Product.Create(expected);
            Product.Save();
            var actual = Product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhereProduct_ThenDeleteProduct()
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
            Product expected = null;

            //Act
            Product.Delete(arbitraryProduct);
            Product.Save();
            var actual = Product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenProduct_ThenUpdateProduct()
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
            Product.Update(arbitraryUpdatedProduct);
            Product.Save();
            var actual = Product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing GetList method.
        /// </summary>
        [Test]
        public void Get_ProductList()
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
            var actual = Product.GetList();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
