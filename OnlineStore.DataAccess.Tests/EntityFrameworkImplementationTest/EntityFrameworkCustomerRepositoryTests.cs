using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess;

namespace OnlineStore.DataAccess.Tests.EntityFrameworkImplementationTests
{
    /// <summary>
    /// EntityFrameworkCustomerRepository tests class.
    /// </summary>
    class EntityFrameworkCustomerRepositoryTests
    {
        /// <summary>
        /// DataBaseContext object.
        /// </summary>
        private DataBaseContext _context;

        /// <summary>
        /// EntityFrameworkCustomerRepository object.
        /// </summary>
        private EntityFrameworkCustomerRepository _customer;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestsDb")
                .Options;
            _context = new DataBaseContext(options);
            _customer = new EntityFrameworkCustomerRepository(_context);
            _context.Database.EnsureCreated();
            SeedDatabase();
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


            _context.Database.EnsureDeleted();
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


            _context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Delete method.
        /// </summary>
        [Test]
        public void Delete_WhenTakesCustomer_ThenDeleteCustomer()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitraryCustomer = _customer.GetEntity(arbitraryId);
            //Сreating an empty object. 
            Customer expected = null;

            //Act
            _customer.Delete(arbitraryId);
            _customer.Save();
            var actual = _customer.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);


            _context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing Update method.
        /// </summary>
        [Test]
        public void Update_WhenTakesCustomer_ThenUpdateCustomer()
        {
            //Arrange
            const int arbitraryId = 1;
            var arbitraryUpdatedCustomer = _customer.GetEntity(arbitraryId);
            arbitraryUpdatedCustomer.FirstName = "Sasha2";
            var expected = arbitraryUpdatedCustomer;

            //Act
            _customer.Update(arbitraryUpdatedCustomer);
            var actual = _customer.GetEntity(arbitraryId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
            

            _context.Database.EnsureDeleted();
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


            _context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    FirstName = "Sasha",
                    LastName = "Zhevak",
                    Address = "Main Street",
                    PhoneNumber = "0669705219"
                },

                new Customer()
                {
                    FirstName = "Andrew",
                    LastName = "Korolenko",
                    Address = "52 Street",
                    PhoneNumber = "0669705345"
                }
            };
            _context.Customers.AddRange(customers);
            _customer.Save();
        }
    }
}
