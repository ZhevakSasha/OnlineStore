using NUnit.Framework;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using System.Collections.Generic;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using OnlineStore.DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.DataAccess.Tests.EntityFrameworkImplementationTests
{
    class EntityFrameworkProductRepositoryTests
    {
        /// <summary>
        /// DataBaseContext object.
        /// </summary>
        private DataBaseContext context;

        /// <summary>
        /// EntityFrameworkProductRepository object.
        /// </summary>
        private EntityFrameworkProductRepository Product;

        /// <summary>
        /// Setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
               .UseInMemoryDatabase(databaseName: "TestsDb")
               .Options;
            context = new DataBaseContext(options);
            Product = new EntityFrameworkProductRepository(context);
            context.Database.EnsureCreated();
            SeedDatabase();
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


            context.Database.EnsureDeleted();
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
            var actual = Product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhereProduct_ThenDeleteProduct()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitraryProduct = Product.GetEntity(arbitraryId);
            //Сreating an empty object. 
            Product expected = null;

            //Act
            Product.Delete(arbitraryProduct);
            Product.Save();
            var actual = Product.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenProduct_ThenUpdateProduct()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitraryUpdatedProduct = Product.GetEntity(arbitraryId);
            arbitraryUpdatedProduct.Price = 400;
            var expected = arbitraryUpdatedProduct;

            //Act
            Product.Update(arbitraryUpdatedProduct);
            Product.Save();
            var actual = Product.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            context.Database.EnsureDeleted();
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


            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var products = new List<Product>()
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
            context.Products.AddRange(products);
            Product.Save();
        }
    }
}
