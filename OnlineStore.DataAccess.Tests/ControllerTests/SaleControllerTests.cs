﻿using System.Collections.Generic;
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
    public class SaleControllerTests
    {

        private Mock<ISaleService> _mockSaleService;

        private Mock<IProductService> _mockProductService;

        private Mock<ICustomerService> _mockCustomerService;

        private SaleController _saleController;

        private IMapper _mapper;

        public SaleControllerTests()
        {
            _mockSaleService = new Mock<ISaleService>();
            _mockProductService = new Mock<IProductService>();
            _mockCustomerService = new Mock<ICustomerService>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();

            _saleController = new SaleController(_mockSaleService.Object, _mockProductService.Object, _mockCustomerService.Object, _mapper);
        }

        [Fact]
        public void SaleTableReturnsAViewResultWithAListOfSales()
        {
            // Arrange
            _mockSaleService.Setup(repo => repo.GetAllSales()).Returns(GetTestSales());

            // Act
            var result = _saleController.SaleTable();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SaleViewModel>>(viewResult.Model);
            Assert.Equal(GetTestSales().Count(), model.Count());
        }

        private List<SaleDto> GetTestSales()
        {
            var sales = new List<SaleDto>
            {
                new SaleDto() { Id=1, CustomerId = 1, ProductId=1, Amount =2, DateOfSale="2021-08-26"},
                new SaleDto() { Id=1, CustomerId = 2, ProductId=2, Amount =1, DateOfSale="2021-07-26"},
                new SaleDto() { Id=1, CustomerId = 2, ProductId=1, Amount =3, DateOfSale="2021-06-26"}
            };
            return sales;
        }

        [Fact]
        public void AddSaleReturnsViewResultWithSaleModel()
        {
            // Arrange

            _saleController.ModelState.AddModelError("Amount", "Required");
            SaleViewModel newSale = new SaleViewModel();

            // Act
            var result = _saleController.SaleCreating(newSale);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewResult?.Model, newSale);
        }

        [Fact]
        public void AddCutomerReturnsARedirectAndAddsSale()
        {
            // Arrange
            var newSale = new SaleViewModel() { Id = 1, CustomerId = 1, ProductId = 1, Amount = 2, DateOfSale = "2021-08-26" };
            _mockSaleService.Setup(r => r.CreateSale(It.IsAny<SaleDto>())).Verifiable();

            // Act
            var result = _saleController.SaleCreating(newSale);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("SaleTable", redirectToActionResult.ActionName);
            _mockSaleService.Verify(r => r.CreateSale(It.IsAny<SaleDto>()));
        }

        [Fact]
        public void UpdateCutomerReturnsARedirectAndUpdatesSale()
        {
            // Arrange
            var newSale = new SaleViewModel() { Id = 1, CustomerId = 1, ProductId = 1, Amount = 2, DateOfSale = "2021-08-26" };
            _mockSaleService.Setup(r => r.UpdateSale(It.IsAny<SaleDto>())).Verifiable();

            // Act
            var result = _saleController.SaleUpdating(newSale);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("SaleTable", redirectToActionResult.ActionName);
            _mockSaleService.Verify(r => r.UpdateSale(It.IsAny<SaleDto>()));
        }

        [Fact]
        public void UpdateSaleReturnsViewResultWithSaleModel()
        {
            // Arrange
            _saleController.ModelState.AddModelError("Amount", "Required");
            var newSale = new SaleViewModel();

            // Act
            var result = _saleController.SaleUpdating(newSale);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newSale, viewResult?.Model);
        }

        [Fact]
        public void DeleteCutomerReturnsARedirectAndDeletesSale()
        {
            // Arrange
            var newSale = new SaleViewModel();
            _mockSaleService.Setup(r => r.DeleteSale(It.IsAny<int>())).Verifiable();

            // Act
            var result = _saleController.SaleDeleting(newSale);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("SaleTable", redirectToActionResult.ActionName);
            _mockSaleService.Verify(r => r.DeleteSale(It.IsAny<int>()));
        }
    }
}