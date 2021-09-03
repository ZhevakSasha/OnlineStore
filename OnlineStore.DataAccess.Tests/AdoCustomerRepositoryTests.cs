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
        IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appconfig.json")
                .Build();
        //string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OnlineStoreDataBase4;Integrated Security=True";
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;




        [SetUp]
        public void Setup()
        {
          
        }

        [Test]
        public void Get_CustomerById_ReturnsFirstName()
        {
            //Arrange
            AdoCustomerRepository customer = new AdoCustomerRepository(configuration["DefaultConnection"]);


            //Act
            var actual = customer.GetEntity(1).FirstName;

            //Assert
            Assert.AreEqual(actual,"Sasha");
        }
    }
}