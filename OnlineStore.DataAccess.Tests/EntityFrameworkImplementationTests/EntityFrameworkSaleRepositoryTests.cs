using NUnit.Framework;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using System.Collections.Generic;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;

namespace OnlineStore.DataAccess.Tests.EntityFrameworkImplementationTests
{
    /// <summary>
    /// EntityFrameworkSaleRepository tests class.
    /// </summary>
    class EntityFrameworkSaleRepositoryTests
    {
        /// <summary>
        /// DataBaseConfiguration object.
        /// </summary>
        private DataBaseConfiguration _dbConfiguration;

        /// <summary>
        /// AdoSaleRepository object.
        /// </summary>
        private EntityFrameworkSaleRepository Sale;

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
            Sale = new EntityFrameworkSaleRepository(connectionString);
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
            Sale.Save();
            var actual = Sale.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhereSale_ThenDeleteSale()
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
            Sale expected = null;

            //Act
            Sale.Delete(arbitrarySale);
            Sale.Save();
            var actual = Sale.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenSale_ThenUpdateSale()
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
            Sale.Update(arbitraryUpdatedSale);
            Sale.Save();
            var actual = Sale.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
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
        }
    }
}
