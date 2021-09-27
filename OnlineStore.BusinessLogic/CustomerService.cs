using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic
{
    /// <summary>
    /// Customer service.
    /// </summary>
    public class CustomerService : ICustomerService
    {

        /// <summary>
        /// Customer repository.
        /// </summary>
        private ICustomerRepository _customer;

        /// <summary>
        /// CustomerService constructor.
        /// </summary>
        /// <param name="customer">Customer repository</param>
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
