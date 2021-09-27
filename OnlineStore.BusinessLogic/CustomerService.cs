using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic
{
    public class CustomerService : ICustomerService
    {

        private ICustomerRepository _customer;

        public CustomerService(ICustomerRepository customer)
        {
            _customer = customer;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customer.GetList();
        }

        public void CreateCustomer(Customer customer)
        {
            _customer.Create(customer);
            _customer.Save();
        }

        public void UpdateCustomer(Customer customer)
        {
            _customer.Update(customer);
            _customer.Save();
        }

        public Customer FindCustomerById(int id)
        {
            return _customer.GetEntity(id);
        }

        public void DeleteCustomer(Customer customer)
        {
            _customer.Delete(customer);
            _customer.Save();
        }
    }
}
