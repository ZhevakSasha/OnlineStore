using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;
using OnlineStore.DataAccess.DataModel;
using Microsoft.Extensions.Configuration;
using FluentAssertions;



namespace OnlineStore.DataAccess.Tests
{
    /// <summary>
    /// Tests class.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// DataBaseConfiguration object.
        /// </summary>
        private DataBaseConfiguration _dbConfiguration;

        /// <summary>
        /// AdoCustomerRepository object.
        /// </summary>
        private AdoCustomerRepository Customer;
        readonly string connectionString = InitConfiguration().GetConnectionString("DefaultConnection");

        /// <summary>
        /// Method for defining the configuration for .json file.
        /// </summary>
        /// <returns>configuration</returns>
        public static IConfiguration InitConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appconfig.json")
                .Build();
            return configuration;
        }

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
            const int constantId = 1;
            var expected = new Customer()
            {
                Id = constantId,
                FirstName = "Sasha",
                LastName = "Zhevak",
                Addres = "Main Street",
                PhoneNumber = "0669705219"
            };

            //Act
            var actual = Customer.GetEntity(constantId);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}