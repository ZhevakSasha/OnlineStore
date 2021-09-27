using OnlineStore.DataAccess.DataModel;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Customer FindCustomerById(int id);
        void DeleteCustomer(Customer customer);

    }
}
