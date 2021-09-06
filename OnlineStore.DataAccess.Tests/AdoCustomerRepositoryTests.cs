using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using System.Configuration;



namespace OnlineStore.DataAccess.Tests
{
    public class Tests
    {

        readonly string connectionString = InitConfiguration().GetConnectionString("DefaultConnection");

        public static IConfiguration InitConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appconfig.json")
                .Build();
            return configuration;
        }

        [Test]
        public void Get_CustomerById_ReturnsCustomer()
        {       
            //Arrange
           
            var customer = new AdoCustomerRepository(connectionString);

            //Act
            var actual = customer.GetEntity(1);
            var expected = new Customer()
            {
                Id = 1,
                FirstName = "Sasha",
                LastName = "Zhevak",
                Addres = "Main Street",
                PhoneNumber = "0669705219"
            };

            //Assert
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Addres, actual.Addres);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);

        }
    }
}