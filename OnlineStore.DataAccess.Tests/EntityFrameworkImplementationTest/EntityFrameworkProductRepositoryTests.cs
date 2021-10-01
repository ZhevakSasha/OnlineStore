using NUnit.Framework;
using OnlineStore.DataAccess.DataModel;
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
        private DataBaseContext _context;

        /// <summary>
        /// EntityFrameworkProductRepository object.
        /// </summary>
        private EntityFrameworkProductRepository _product;

        /// <summary>
        /// Setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
               .UseInMemoryDatabase(databaseName: "TestsDb")
               .Options;
            _context = new DataBaseContext(options);
            _product = new EntityFrameworkProductRepository(_context);
            _context.Database.EnsureCreated();
            SeedDatabase();
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


            _context.Database.EnsureDeleted();
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
                ProductName = "Phone",
                Price = 180,
                UnitOfMeasurement = "pc."
            };

            //Act
            _product.Create(expected);
            var actual = _product.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            _context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhenTakesProduct_ThenDeleteProduct()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitraryProduct = _product.GetEntity(arbitraryId);
            //Сreating an empty object. 
            Product expected = null;

            //Act
            _product.Delete(arbitraryId);
            _product.Save();
            var actual = _product.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            _context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenTakesProduct_ThenUpdateProduct()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitraryUpdatedProduct = _product.GetEntity(arbitraryId);
            arbitraryUpdatedProduct.Price = 400;
            var expected = arbitraryUpdatedProduct;

            //Act
            _product.Update(arbitraryUpdatedProduct);
            _product.Save();
            var actual = _product.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            _context.Database.EnsureDeleted();
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


            _context.Database.EnsureDeleted();
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
            _context.Products.AddRange(products);
            _product.Save();
        }
    }
}
