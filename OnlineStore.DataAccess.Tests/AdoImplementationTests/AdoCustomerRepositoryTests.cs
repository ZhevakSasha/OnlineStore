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
        private AdoCustomerRepository _customer;

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
            _customer = new AdoCustomerRepository(connectionString);
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
                Address = "Main Street",
                PhoneNumber = "0669705219"
            };

            //Act
            var actual = _customer.GetEntity(concreteId);

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
                Address = "52 Street",
                PhoneNumber = "0662305345"
            };

            //Act
            _customer.Create(expected);
            var actual = _customer.GetEntity(concreteId);

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
                Address = "52 Street",
                PhoneNumber = "0662305345"
            };
            //�reating an empty object. 
            var expected = new Customer();

            //Act
            _customer.Delete(concreteId);
            var actual = _customer.GetEntity(concreteId);

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
                Address = "52 Street",
                PhoneNumber = "0669705345"
            };
            var expected = arbitraryUpdatedCustomer;

            //Act
            _customer.Update(arbitraryUpdatedCustomer);
            var actual = _customer.GetEntity(concreteId);

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
                    Address = "Main Street",
                    PhoneNumber = "0669705219"
                },
                new Customer()
                {
                    Id = 2,
                    FirstName = "Andrew",
                    LastName = "Korolenko",
                    Address = "52 Street",
                    PhoneNumber = "0669705345"
                }           
            };

            //Act
            var actual = _customer.GetList();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}