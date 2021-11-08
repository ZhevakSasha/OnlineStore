using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.MvcApplication.Controllers;
using OnlineStore.BusinessLogic.DtoModels;
using AutoMapper;
using Xunit;
using System.Linq;
using OnlineStore.MvcApplication;
using OnlineStore.MvcApplication.Models;

namespace OnlineStore.DataAccess.Tests.ControllerTests
{
    /// <summary>
    /// Customer controller tests.
    /// </summary>
    public class CustomerControllerTests
    {
        /// <summary>
        /// Mock service object.
        /// </summary>
        private Mock<ICustomerService> _mockService;

        /// <summary>
        /// Customer controller object.
        /// </summary>
        private CustomerController _customerController;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Customer controller constructor.
        /// </summary>
        public CustomerControllerTests()
        {
            _mockService = new Mock<ICustomerService>() { };
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();

            _customerController = new CustomerController(_mockService.Object, _mapper);
        }

        /// <summary>
        /// Customer table View test.
        /// </summary>
        [Fact]
        public void CustomerTableReturnsAViewResultWithAListOfCustomers()
        {
            // Arrange
            _mockService.Setup(repo => repo.GetAllCustomers()).Returns(GetTestCustomers());

            // Act
            var result = _customerController.CustomerTable();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CustomerViewModel>>(viewResult.Model);
            Assert.Equal(GetTestCustomers().Count(), model.Count());
        }

        /// <summary>
        /// Fake GetAllCustomers method.
        /// </summary>
        /// <returns>CustomersDto list</returns>
        private List<CustomerDto> GetTestCustomers()
        {
            var customers = new List<CustomerDto>
            {
                new CustomerDto() { Id=1, FirstName="c", LastName="cus1", Address="ad1", PhoneNumber="0669705219"},
                new CustomerDto() { Id=2, FirstName="c", LastName="cus2", Address="ad2", PhoneNumber="0669705219"},
                new CustomerDto() { Id=3, FirstName="c", LastName="cus3", Address="ad3", PhoneNumber="0669705219"}
            };
            return customers;
        }

        /// <summary>
        /// Testing operability of error model when customer creating.
        /// </summary>
        [Fact]
        public void AddCustomerReturnsViewResultWithCustomerModel()
        {
            // Arrange
            
            _customerController.ModelState.AddModelError("FirstName", "Required");
            CustomerViewModel newCustomer = new CustomerViewModel();

            // Act
            var result = _customerController.CustomerCreating(newCustomer);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(It.IsAny<CustomerViewModel>(), viewResult?.Model);
        }

        /// <summary>
        /// Testing customer creating and redirecting to CustomerTable view.
        /// </summary>
        [Fact]
        public void AddCutomerReturnsARedirectAndAddsCustomer()
        {
            // Arrange
            var newCustomer = new CustomerViewModel() { FirstName = "c", LastName = "c1", Address = "a1", PhoneNumber = "0669705219" };
            _mockService.Setup(r => r.CreateCustomer(It.IsAny<CustomerDto>())).Verifiable();

            // Act
            var result = _customerController.CustomerCreating(newCustomer);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("CustomerTable", redirectToActionResult.ActionName);
            _mockService.Verify(r => r.CreateCustomer(It.IsAny<CustomerDto>()));
        }
       
        /// <summary>
        /// Testing customer updating and redirecting to CustomerTable view.
        /// </summary>
        [Fact]
        public void UpdateCutomerReturnsARedirectAndUpdatesCustomer()
        {
            // Arrange
            var newCustomer = new CustomerViewModel() { FirstName = "c", LastName = "c1", Address = "a1", PhoneNumber = "0669705219" };
            _mockService.Setup(r => r.UpdateCustomer(It.IsAny<CustomerDto>())).Verifiable();

            // Act
            var result = _customerController.CustomerUpdating(newCustomer);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("CustomerTable", redirectToActionResult.ActionName);
            _mockService.Verify(r => r.UpdateCustomer(It.IsAny<CustomerDto>()));
        }

        /// <summary>
        /// Testing operability of error model when customer updating.
        /// </summary>
        [Fact]
        public void UpdateCustomerReturnsViewResultWithCustomerModel()
        {
            // Arrange
            _customerController.ModelState.AddModelError("FirstName", "Required");
            CustomerViewModel newCustomer = new CustomerViewModel();

            // Act
            var result = _customerController.CustomerUpdating(newCustomer);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(It.IsAny<CustomerViewModel>(), viewResult?.Model);
        }

        /// <summary>
        /// Testing customer deleting and redirecting to CustomerTable view.
        /// </summary>
        [Fact]
        public void DeleteCutomerReturnsARedirectAndDeletesCustomer()
        {
            // Arrange
            var newCustomer = new CustomerViewModel();
            _mockService.Setup(r => r.DeleteCustomer(It.IsAny<int>())).Verifiable();

            // Act
            var result = _customerController.CustomerDeleting(newCustomer);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("CustomerTable", redirectToActionResult.ActionName);
            _mockService.Verify(r => r.DeleteCustomer(It.IsAny<int>()));
        }
    }
}
