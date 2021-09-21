using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using System.Collections.Generic;

namespace OnlineStore.DataAccess.Tests
{
    /// <summary>
    /// AdoCustomerRepository tests class.
    /// </summary>
    public class AdoCustomerRepositoryTests
    {
        /// <summary>
        /// DataBaseConfiguration object.
        /// </summary>
        private DataBaseConfiguration _dbConfiguration;

        /// <summary>
        /// AdoCustomerRepository object.
        /// </summary>
        private AdoCustomerRepository _ñustomer;

        /// <summary>
        /// IConfiguration field.
        /// </summary>
        private IConfiguration _ñonfiguration;

        /// <summary>
        /// Setup method.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _ñonfiguration = new ConfigurationBuilder()
             .AddJsonFile(path: "appconfig.json")
             .Build();

            _dbConfiguration = new DataBaseConfiguration(_ñonfiguration);
            _dbConfiguration.DeployTestDatabase();

            var connectionString = _ñonfiguration.GetConnectionString("DefaultConnection");
            _ñustomer = new AdoCustomerRepository(connectionString);
        }

        /// <summary>
        /// Testing GetEntity method.
        /// </summary>
        [Test]
        public void Get_WhenTakesCustomerId_ThenReturnsCustomer()
        {
            //Arrange
            const int concreteId = 1;
            var expected = new Customer()
            {
                Id = concreteId,
                FirstName = "Sasha",
                LastName = "Zhevak",
                Addres = "Main Street",
                PhoneNumber = "0669705219"
            };

            //Act
            var actual = _ñustomer.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Create method.
        /// </summary>
        [Test]
        public void Create_WhenTakesCustomer_ThenCreateCustomer()
        {
            //Arrange
            const int concreteId = 3;
            var expected = new Customer()
            {
                Id = concreteId,
                FirstName = "Anton",
                LastName = "Ivanov",
                Addres = "52 Street",
                PhoneNumber = "0662305345"
            };

            //Act
            _ñustomer.Create(expected);
            var actual = _ñustomer.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhenTakesCustomer_ThenDeleteCustomer() 
        {
            //Arrange
            const int concreteId = 2;
            var arbitraryCustomer = new Customer()
            {
                Id = concreteId,
                FirstName = "Anton",
                LastName = "Ivanov",
                Addres = "52 Street",
                PhoneNumber = "0662305345"
            };
            //Ñreating an empty object. 
            var expected = new Customer();

            //Act
            _ñustomer.Delete(arbitraryCustomer);
            var actual = _ñustomer.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenTakesCustomer_ThenUpdateCustomer()
        {
            //Arrange
            const int concreteId = 2;
            var arbitraryUpdatedCustomer = new Customer()
            {
                Id = concreteId,
                FirstName = "Andrew2",
                LastName = "Korolenko",
                Addres = "52 Street",
                PhoneNumber = "0669705345"
            };
            var expected = arbitraryUpdatedCustomer;

            //Act
            _ñustomer.Update(arbitraryUpdatedCustomer);
            var actual = _ñustomer.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);

            //Drop database.
            _dbConfiguration.DropTestDatabase();
        }

        /// <summary>
        /// Testing GetList method.
        /// </summary>
        [Test]
        public void Get_ReturnCustomerList()
        {
            //Arrange
            var expected = new List<Customer>()
            {
                new Customer()
                {
                    Id = 1,
                    FirstName = "Sasha",
                    LastName = "Zhevak",
                    Addres = "Main Street",
                    PhoneNumber = "0669705219"
                },
                new Customer()
                {
                    Id = 2,
                    FirstName = "Andrew",
                    LastName = "Korolenko",
                    Addres = "52 Street",
                    PhoneNumber = "0669705345"
                }           
            };

            //Act
            var actual = _ñustomer.GetList();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}