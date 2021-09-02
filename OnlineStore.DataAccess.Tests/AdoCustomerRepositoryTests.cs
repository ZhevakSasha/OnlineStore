using NUnit.Framework;
using OnlineStore.DataAccess.AdoRepositoryImplementation;
using OnlineStore.DataAccess.DataModel;

namespace OnlineStore.DataAccess.Tests
{
    public class Tests
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OnlineStoreDataBAse;Integrated Security=True";
        AdoCustomerRepository customer1;


        [SetUp]
        public void Setup()
        {
           // customer = new AdoCustomerRepository(@"Data Source=(LocalDb)\MSSQLLocalDB;database=OnlineStoreDataBase4.dbo;trusted_connection=yes;");
        }

        [Test]
        public void GetEntityTest()
        {
            //customer1 = new AdoCustomerRepository(@"Data Source=(LocalDb)\MSSQLLocalDB;database=OnlineStoreDataBase4.dbo;trusted_connection=True;");
            
            customer1 = new AdoCustomerRepository(connectionString);
            Assert.AreEqual(customer1.GetEntity(1).FirstName,"Sasha");
        }
    }
}