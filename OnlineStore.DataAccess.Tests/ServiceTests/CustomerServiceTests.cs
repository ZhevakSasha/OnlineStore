using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using OnlineStore.BusinessLogic;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using OnlineStore.MvcApplication;

namespace OnlineStore.DataAccess.Tests.ServiceTests
{
    public class CustomerServiceTests
    {
        private ICustomerService _customerService;

        private Mock<ICustomerRepository> _mockRepository;

        private IMapper _mapper;


        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _customerService = new CustomerService(_mockRepository.Object, _mapper);
        }

        [Test]
        public void GetAllCustomers_ReturnsAIEnumerable<CustomerDto>()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetList()).Returns(GetTestCustomers());

            // Act
            var result = _customerService.GetAllCustomers();

            // Assert

            Assert.AreEqual(GetTestCustomers().Count(),result.Count());

        }

        private List<Customer> GetTestCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer() { Id=1, FirstName="c", LastName="cus1", Address="ad1", PhoneNumber="0669705219"},
                new Customer() { Id=2, FirstName="c", LastName="cus2", Address="ad2", PhoneNumber="0669705219"},
                new Customer() { Id=3, FirstName="c", LastName="cus3", Address="ad3", PhoneNumber="0669705219"}
            };
            return customers;
        }

    }
}
