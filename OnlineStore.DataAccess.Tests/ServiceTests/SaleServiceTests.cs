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
    /// <summary>
    /// Sale service tests.
    /// </summary>
    public class SaleServiceTests
    {

        /// <summary>
        /// Sale service object.
        /// </summary>
        private ISaleService _saleService;

        /// <summary>
        /// Mock sale repository object.
        /// </summary>
        private Mock<ISaleRepository> _mockRepository;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Setup method.
        /// </summary>
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

        /// <summary>
        /// Testing GetAllSales method.
        /// </summary>
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

        /// <summary>
        /// Fake GetList method.
        /// </summary>
        /// <returns>Sales list</returns>
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

        /// <summary>
        /// Testing GetSaleById method.
        /// </summary>
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

        /// <summary>
        /// Testing CreateSale method.
        /// </summary>
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

        /// <summary>
        /// Testing DeleteSale method.
        /// </summary>
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

        /// <summary>
        /// Testing UpdateSale method.
        /// </summary>
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