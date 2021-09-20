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
    /// <summary>
    /// EntityFrameworkSaleRepository tests class.
    /// </summary>
    class EntityFrameworkSaleRepositoryTests
    {
        /// <summary>
        /// DataBaseContext object.
        /// </summary>
        private DataBaseContext context;

        /// <summary>
        /// AdoSaleRepository object.
        /// </summary>
        private EntityFrameworkSaleRepository Sale;

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
            Sale = new EntityFrameworkSaleRepository(context);
            context.Database.EnsureCreated();
            SeedDatabase();
        }

        /// <summary>
        /// Testing GetEntity method.
        /// </summary>
        [Test]
        public void Get_WhereSaleById_ThenReturnsSale()
        {
            //Arrange
            const int concreteId = 1;
            var expected = new Sale()
            {
                Id = concreteId,
                ProductId = 1,
                CustomerId = 1,
                DateOfSale = "25.08.2021",
                Amount = 2
            };

            //Act
            var actual = Sale.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Create method.
        /// </summary>
        [Test]
        public void Create_WhenSale_ThenCreateSale()
        {
            //Arrange
            const int concreteId = 3;
            var expected = new Sale()
            {
                ProductId = 1,
                CustomerId = 2,
                DateOfSale = "27.0",
                Amount = 4
            };

            //Act
            Sale.Create(expected);
            var actual = Sale.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhereSale_ThenDeleteSale()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitrarySale = Sale.GetEntity(arbitraryId);
            //Сreating an empty object. 
            Sale expected = null;

            //Act
            Sale.Delete(arbitrarySale);
            Sale.Save();
            var actual = Sale.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenSale_ThenUpdateSale()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitraryUpdatedSale = Sale.GetEntity(arbitraryId);
            arbitraryUpdatedSale.DateOfSale = "20.09.2021";
            var expected = arbitraryUpdatedSale;

            //Act
            Sale.Update(arbitraryUpdatedSale);
            Sale.Save();
            var actual = Sale.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing GetList method.
        /// </summary>
        [Test]
        public void Get_SaleList()
        {
            //Arrange
            var expected = new List<Sale>()
            {
                new Sale()
                {
                Id = 1,
                ProductId = 1,
                CustomerId = 1,
                DateOfSale = "25.08.2021",
                Amount = 2
                },
                new Sale()
                {
                Id = 2,
                ProductId = 2,
                CustomerId = 2,
                DateOfSale = "26.08.2021",
                Amount = 3
                }
            };

            //Act
            var actual = Sale.GetList();

            //Assert
            actual.Should().BeEquivalentTo(expected);


            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var sales = new List<Sale>()
            {
                new Sale()
                {
                Id = 1,
                ProductId = 1,
                CustomerId = 1,
                DateOfSale = "25.08.2021",
                Amount = 2
                },

                new Sale()
                {
                Id = 2,
                ProductId = 2,
                CustomerId = 2,
                DateOfSale = "26.08.2021",
                Amount = 3
                }
            };
            context.Sales.AddRange(sales);
            Sale.Save();
        }
    }
}
