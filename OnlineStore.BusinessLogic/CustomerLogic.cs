using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.EntityFrameworkRepositoryImplementation;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic
{
    public class CustomerLogic
    {

        /// <summary>
        /// Context field.
        /// </summary>
        private readonly DataBaseContext _context;

        private EntityFrameworkCustomerRepository _customer;

        public CustomerLogic(DataBaseContext context)
        {
            _context = context;
            _customer = new EntityFrameworkCustomerRepository(_context);
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
