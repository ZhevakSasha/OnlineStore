using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OnlineStore.BusinessLogic;
using OnlineStore.BusinessLogic.DtoModels;
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
        public void GetAllCustomers_ReturnsAIEnumerableCustomersDto()
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

        [Test]
        public void GetCustomerById_ReturnsCustomerDtoById()
        {
            // Arrange
            const int arbitraryId = 1;
            var expected = new Customer()
            {
                Id = arbitraryId,
                FirstName = "c",
                LastName = "cus1",
                Address = "ad1",
                PhoneNumber = "0669705219"
            };
            _mockRepository.Setup(repo => repo.GetEntity(arbitraryId)).Returns(expected);

            // Act
            var result = _customerService.FindCustomerById(arbitraryId);

            // Assert
            result.Should().BeEquivalentTo(_mapper.Map<CustomerDto>(expected));
        }

        [Test]
        public void CreateCustomer_ChecksTheCallOfCreatingMethod()
        {
            // Arrange
            const int arbitraryId = 1;
            var expected = new Customer()
            {
                Id = arbitraryId,
                FirstName = "c",
                LastName = "cus1",
                Address = "ad1",
                PhoneNumber = "0669705219"
            };
            _mockRepository.Setup(repo => repo.Create(It.IsAny<Customer>())).Verifiable();
            
            // Act
            _customerService.CreateCustomer(_mapper.Map<CustomerDto>(expected));

            // Assert
            _mockRepository.Verify(repo => repo.Create(It.IsAny<Customer>()), Times.Once);

        }

        [Test]
        public void DeleteCustomer_ChecksTheCallOfDeletingMethod()
        {
            // Arrange
            const int arbitraryId = 1;
            _mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Verifiable();

            // Act
            _customerService.DeleteCustomer(arbitraryId);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void UpdateCustomer_ChecksTheCallOfUpdateMethod()
        {
            // Arrange
            const int arbitraryId = 1;
            var expected = new Customer()
            {
                Id = arbitraryId,
                FirstName = "c",
                LastName = "cus1",
                Address = "ad1",
                PhoneNumber = "0669705219"
            };
            _mockRepository.Setup(repo => repo.Update(It.IsAny<Customer>())).Verifiable();

            // Act
            _customerService.UpdateCustomer(_mapper.Map<CustomerDto>(expected));

            // Assert
            _mockRepository.Verify(repo => repo.Update(It.IsAny<Customer>()), Times.Once);

        }


    }
}
