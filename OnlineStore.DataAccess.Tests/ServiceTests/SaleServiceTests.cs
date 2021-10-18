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
    public class SaleServiceTests
    {
        private ISaleService _saleService;

        private Mock<ISaleRepository> _mockRepository;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ISaleRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _saleService = new SaleService(_mockRepository.Object, _mapper);
        }

        [Test]
        public void GetAllSales_ReturnsAIEnumerableSalesDto()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetList()).Returns(GetTestSales());

            // Act
            var result = _saleService.GetAllSales();

            // Assert
            Assert.AreEqual(GetTestSales().Count(), result.Count());

        }

        private List<Sale> GetTestSales()
        {
            var sales = new List<Sale>
            {
                new Sale() { Id=1, CustomerId = 1, ProductId=1, Amount =2, DateOfSale="2021-08-26"},
                new Sale() { Id=1, CustomerId = 2, ProductId=2, Amount =1, DateOfSale="2021-07-26"},
                new Sale() { Id=1, CustomerId = 2, ProductId=1, Amount =3, DateOfSale="2021-06-26"}
            };
            return sales;
        }

        [Test]
        public void GetSaleById_ReturnsSaleDtoById()
        {
            // Arrange
            const int arbitraryId = 1;
            var expected = new Sale()
            {
                Id = 1,
                CustomerId = 1,
                ProductId = 1,
                Amount = 2,
                DateOfSale = "2021-08-26"
            };
            _mockRepository.Setup(repo => repo.GetEntity(arbitraryId)).Returns(expected);

            // Act
            var result = _saleService.FindSaleById(arbitraryId);

            // Assert
            result.Should().BeEquivalentTo(_mapper.Map<SaleDto>(expected));
        }

        [Test]
        public void CreateSale_ChecksTheCallOfCreatingMethod()
        {
            // Arrange
            const int arbitraryId = 1;
            var expected = new Sale()
            {
                Id = arbitraryId,
                CustomerId = 1,
                ProductId = 1,
                Amount = 2,
                DateOfSale = "2021-08-26"
            };
            _mockRepository.Setup(repo => repo.Create(It.IsAny<Sale>())).Verifiable();

            // Act
            _saleService.CreateSale(_mapper.Map<SaleDto>(expected));

            // Assert
            _mockRepository.Verify(repo => repo.Create(It.IsAny<Sale>()), Times.Once);

        }

        [Test]
        public void DeleteSale_ChecksTheCallOfDeletingMethod()
        {
            // Arrange
            const int arbitraryId = 1;
            _mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Verifiable();

            // Act
            _saleService.DeleteSale(arbitraryId);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void UpdateSale_ChecksTheCallOfUpdateMethod()
        {
            // Arrange
            const int arbitraryId = 1;
            var expected = new Sale()
            {
                Id = arbitraryId,
                CustomerId = 1,
                ProductId = 1,
                Amount = 2,
                DateOfSale = "2021-08-26"
            };
            _mockRepository.Setup(repo => repo.Update(It.IsAny<Sale>())).Verifiable();

            // Act
            _saleService.UpdateSale(_mapper.Map<SaleDto>(expected));

            // Assert
            _mockRepository.Verify(repo => repo.Update(It.IsAny<Sale>()), Times.Once);

        }
    }
}