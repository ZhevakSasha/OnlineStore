using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;

namespace OnlineStore.DataAccess.Tests
{
    public class Tests
    {

        AdoCustomerRepository customer;

        [SetUp]
        public void Setup()
        {
            customer = new AdoCustomerRepository(@"Data Source=(LocalDb)\MSSQLLocalDB;database=OnlineStoreDataBase4.dbo;trusted_connection=yes;");
        }

        [Test]
        public void AdoRepositoryImplementationTest()
        {
            Assert.Pass();
        }
    }
}