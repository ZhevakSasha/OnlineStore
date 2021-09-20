using NUnit.Framework;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using System.Collections.Generic;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.DataAccess;
using System.Linq;

namespace OnlineStore.DataAccess.Tests.EntityFrameworkImplementationTests
{
    /// <summary>
    /// EntityFrameworkCustomerRepository tests class.
    /// </summary>
    class EntityFrameworkCustomerRepositoryTests
    {
        /// <summary>
        /// DataBaseConfiguration object.
        /// </summary>
        private DataBaseConfiguration _dbConfiguration;

        DataBaseContext context;

        /// <summary>
        /// EntityFrameworkCustomerRepository object.
        /// </summary>
        private EntityFrameworkCustomerRepository Customer;

        /// <summary>
        /// Setup method.
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestsDb")
                .Options;
            context = new DataBaseContext(options);
            Customer = new EntityFrameworkCustomerRepository(context);
            context.Database.EnsureCreated();
            SeedDatabase();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
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
            const int concreteId = 3;
            var arbitraryCustomer = context.Customers.LastOrDefault();
            //Сreating an empty object. 
            Customer expected = null;

            //Act
            Customer.Delete(arbitraryCustomer);
            Customer.Save();

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
            const int concreteId = 1;
            var arbitraryUpdatedCustomer = context.Customers.FirstOrDefault();
            arbitraryUpdatedCustomer.FirstName = "Sasha2";
            var expected = arbitraryUpdatedCustomer;

            //Act
            Customer.Update(arbitraryUpdatedCustomer);
            var actual = Customer.GetEntity(concreteId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
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

        private void SeedDatabase()
        {
            var customers = new List<Customer>()
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
            context.Customers.AddRange(customers);
            Customer.Save();

        }
    }
}
