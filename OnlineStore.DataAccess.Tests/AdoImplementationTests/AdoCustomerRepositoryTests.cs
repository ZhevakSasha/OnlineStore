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
        private AdoCustomerRepository Customer;

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
            Customer = new AdoCustomerRepository(connectionString);
        }

        /// <summary>
        /// Testing GetEntity method.
        /// </summary>
        [Test]
        public void Get_WhereCustomerById_ThenReturnsCustomer()
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
            var actual = Customer.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Create method.
        /// </summary>
        [Test]
        public void Create_WhenCustomer_ThenCreateCustomer()
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
            Customer.Create(expected);
            var actual = Customer.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhereCustomer_ThenDeleteCustomer() 
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
            Customer.Delete(arbitraryCustomer);
            var actual = Customer.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenCustomer_ThenUpdateCustomer()
        {
            //Arrange
            const int concreteId = 2;
            var arbitraryUpdatedCustomer = new Customer()
            {
                Id = concreteId,
                FirstName = "Andrew",
                LastName = "Korolenko",
                Addres = "52 Street",
                PhoneNumber = "0669705345"
            };
            var expected = arbitraryUpdatedCustomer;
           
            //Act
            Customer.Update(arbitraryUpdatedCustomer);
            var actual = Customer.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);

            //Drop database.
            _dbConfiguration.DropTestDatabase();
        }

        /// <summary>
        /// Testing GetList method.
        /// </summary>
        [Test]
        public void Get_CustomerList()
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
            var actual = Customer.GetList();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}