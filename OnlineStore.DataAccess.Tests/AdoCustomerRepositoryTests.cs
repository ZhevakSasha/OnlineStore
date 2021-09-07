using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using FluentAssertions;



namespace OnlineStore.DataAccess.Tests
{
    public class Tests
    {
        private AdoCustomerRepository Customer;

        readonly string connectionString = InitConfiguration().GetConnectionString("DefaultConnection");

        public static IConfiguration InitConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appconfig.json")
                .Build();
            return configuration;
        }

        private DataBaseConfiguration _dbConfiguration;


        [SetUp]
        public void Setup()
        {

            _dbConfiguration = new DataBaseConfiguration(InitConfiguration());
            _dbConfiguration.DeployTestDatabase();

            var connectionString = InitConfiguration().GetConnectionString("DefaultConnection");
            Customer = new AdoCustomerRepository(connectionString);

        }

        [Test]
        public void Get_CustomerById_ReturnsCustomer()
        {       
            //Arrange
           
            Customer = new AdoCustomerRepository(connectionString);
            var expected = new Customer()
            {
                Id = 1,
                FirstName = "Sasha",
                LastName = "Zhevak",
                Addres = "Main Street",
                PhoneNumber = "0669705219"
            };

            //Act
            var actual = Customer.GetEntity(1);


            //Assert

            actual.Should().BeEquivalentTo(expected);

        }
    }
}