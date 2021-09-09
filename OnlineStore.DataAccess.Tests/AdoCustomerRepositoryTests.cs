using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using System.Collections.Generic;

namespace OnlineStore.DataAccess.Tests
{
    /// <summary>
    /// Tests class.
    /// </summary>
    public class Tests
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
        /// Method for defining the configuration for .json file.
        /// </summary>
        /// <returns>configuration</returns>
        public static IConfiguration InitConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appconfig.json")
                .Build();
            return configuration;
        }

        [SetUp]
        public void Setup()
        {
            _dbConfiguration = new DataBaseConfiguration(InitConfiguration());
            _dbConfiguration.DeployTestDatabase();

            var connectionString = InitConfiguration().GetConnectionString("DefaultConnection");
            Customer = new AdoCustomerRepository(connectionString);
        }

        /// <summary>
        /// Testing GetEntity method.
        /// </summary>
        [Test]
        public void Get_CustomerById_ReturnsCustomer()
        {
            //Arrange
            const int constantId = 1;
            var expected = new Customer()
            {
                Id = constantId,
                FirstName = "Sasha",
                LastName = "Zhevak",
                Addres = "Main Street",
                PhoneNumber = "0669705219"
            };

            //Act
            var actual = Customer.GetEntity(constantId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Create method
        /// </summary>
        [Test]
        public void Create_Customer()
        {
            //Arrange
            const int constantId = 2;
            var expected = new Customer()
            {
                Id = constantId,
                FirstName = "Andrew",
                LastName = "Korolenko",
                Addres = "52 Street",
                PhoneNumber = "0669705345"
            };

            //Act
            Customer.Create(expected);
            var actual = Customer.GetEntity(constantId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_Customer() 
        {
            //Arrange
            const int constantId = 2;
            var constantCustomer = new Customer()
            {
                Id = constantId,
                FirstName = "Andrew",
                LastName = "Korolenko",
                Addres = "52 Street",
                PhoneNumber = "0669705345"
            };
            //Ñreating an empty object. 
            var expected = new Customer();

            //Act
            Customer.Delete(constantCustomer);
            var actual = Customer.GetEntity(constantId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_Customer()
        {
            //Arrange
            const int constantId = 2;
            var constantCustomer = new Customer()
            {
                Id = constantId,
                FirstName = "Andrew",
                LastName = "Korolenko",
                Addres = "52 Street",
                PhoneNumber = "0669705345"
            };
            var constantUpdatedCustomer = new Customer()
            {
                Id = constantId,
                FirstName = "Anton",
                LastName = "Ivanov",
                Addres = "52 Street",
                PhoneNumber = "0662305345"
            };
            var expected = constantUpdatedCustomer;
           
            //Act
            Customer.Create(constantCustomer);
            Customer.Update(constantUpdatedCustomer);
            var actual = Customer.GetEntity(constantId);
            actual.Should().BeEquivalentTo(expected);

        }

        /// <summary>
        /// Testing GetList method.
        /// </summary>
        [Test]
        public void Get_CustomerList()
        {
            //Arrange
            var expected = new List<Customer>();

            //Act
            expected.Add(new Customer()
            {
                Id = 1,
                FirstName = "Sasha",
                LastName = "Zhevak",
                Addres = "Main Street",
                PhoneNumber = "0669705219"
            });
            expected.Add(new Customer()
            {
                Id = 2,
                FirstName = "Andrew",
                LastName = "Korolenko",
                Addres = "52 Street",
                PhoneNumber = "0669705345"
            });

            //Assert
            Customer.Create(new Customer()
            {
                Id = 2,
                FirstName = "Andrew",
                LastName = "Korolenko",
                Addres = "52 Street",
                PhoneNumber = "0669705345"
            });
            var actual = Customer.GetList();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}