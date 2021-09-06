using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Extensions.Configuration.Json;

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
        public void Get_CustomerById_ReturnsFirstName()
        {       
            //Arrange
            AdoCustomerRepository customer = new AdoCustomerRepository(connectionString);

            //Act
            var actual = customer.GetEntity(1).FirstName;

            //Assert
            Assert.AreEqual(actual,"Sasha");
        }
    }
}