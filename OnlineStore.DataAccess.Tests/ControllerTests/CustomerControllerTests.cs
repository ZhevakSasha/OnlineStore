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
    public class CustomerControllerTests
    {

        private Mock<ICustomerService> _mockService;

        private CustomerController _customerController;

        private IMapper _mapper;

        public CustomerControllerTests()
        {
            _mockService = new Mock<ICustomerService>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();

            _customerController = new CustomerController(_mockService.Object, _mapper);
        }

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
            Assert.Equal(newCustomer, viewResult?.Model);
        }

        [Fact]
        public void AddCutomerReturnsARedirectAndAddsCustomer()
        {
            // Arrange
            var newCustomer = new CustomerViewModel() { FirstName = "c", LastName = "c1", Address = "a1", PhoneNumber = "0669705219" };

            // Act
            var result = _customerController.CustomerCreating(newCustomer);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("CustomerTable", redirectToActionResult.ActionName);
            _mockService.Verify(r => r.CreateCustomer(_mapper.Map<CustomerDto>(newCustomer)));
        }


    }
}
