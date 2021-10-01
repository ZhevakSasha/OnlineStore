using NUnit.Framework;
using OnlineStore.DataAccess.DataModel;
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
        private DataBaseContext _context;

        /// <summary>
        /// AdoSaleRepository object.
        /// </summary>
        private EntityFrameworkSaleRepository _sale;

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
            _sale = new EntityFrameworkSaleRepository(_context);
            _context.Database.EnsureCreated();
            SeedDatabase();
        }

        /// <summary>
        /// Testing GetEntity method.
        /// </summary>
        [Test]
        public void Get_WhenTakesSaleId_ThenReturnsSale()
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
            var actual = _sale.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            _context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Create method.
        /// </summary>
        [Test]
        public void Create_WhenTakesSale_ThenCreateSale()
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
            _sale.Create(expected);
            var actual = _sale.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            _context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhenTakesSale_ThenDeleteSale()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitrarySale = _sale.GetEntity(arbitraryId);
            //Сreating an empty object. 
            Sale expected = null;

            //Act
            _sale.Delete(arbitraryId);
            _sale.Save();
            var actual = _sale.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            _context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenTakesSale_ThenUpdateSale()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitraryUpdatedSale = _sale.GetEntity(arbitraryId);
            arbitraryUpdatedSale.DateOfSale = "20.09.2021";
            var expected = arbitraryUpdatedSale;

            //Act
            _sale.Update(arbitraryUpdatedSale);
            _sale.Save();
            var actual = _sale.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            _context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing GetList method.
        /// </summary>
        [Test]
        public void Get_ReturnSaleList()
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
            var actual = _sale.GetList();

            //Assert
            actual.Should().BeEquivalentTo(expected);


            _context.Database.EnsureDeleted();
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
            _context.Sales.AddRange(sales);
            _sale.Save();
        }
    }
}
