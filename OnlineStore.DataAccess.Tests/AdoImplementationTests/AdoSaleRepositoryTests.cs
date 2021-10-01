using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;
using OnlineStore.DataAccess.DataModel;
using System.Collections.Generic;

namespace OnlineStore.DataAccess.Tests
{
    /// <summary>
    /// AdoSaleRepository tests class.
    /// </summary>
    class AdoSaleRepositoryTests
    {
        /// <summary>
        /// DataBaseConfiguration object.
        /// </summary>
        private DataBaseConfiguration _dbConfiguration;

        /// <summary>
        /// AdoSaleRepository object.
        /// </summary>
        private AdoSaleRepository _sale;

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
            _sale = new AdoSaleRepository(connectionString);
        }

        /// <summary>
        /// Testing GetEntity method.
        /// </summary>
        [Test]
        public void Get_WhenTakesSale_ThenReturnsSale()
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
                Id = concreteId,
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
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhenTakesSale_ThenDeleteSale()
        {
            //Arrange
            const int concreteId = 2;
            var arbitrarySale = new Sale()
            {
                Id = concreteId,
                ProductId = 2,
                CustomerId = 2,
                DateOfSale = "26.08.2021",
                Amount = 3
            };
            //Сreating an empty object. 
            var expected = new Sale();

            //Act
            _sale.Delete(concreteId);
            var actual = _sale.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenTakesSale_ThenUpdateSale()
        {
            //Arrange
            const int concreteId = 2;
            var arbitraryUpdatedSale = new Sale()
            {
                Id = concreteId,
                ProductId = 2,
                CustomerId = 2,
                DateOfSale = "26.08.2021",
                Amount = 7
            };
            var expected = arbitraryUpdatedSale;

            //Act
            _sale.Update(arbitraryUpdatedSale);
            var actual = _sale.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
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
        }
    }
}
