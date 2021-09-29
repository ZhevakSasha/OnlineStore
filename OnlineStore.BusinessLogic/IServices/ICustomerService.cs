using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.DataAccess.DataModel;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.IServices
{
    /// <summary>
    /// CustomerService interface.
    /// </summary>
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAllCustomers();
        void CreateCustomer(CustomerDto customer);
        void UpdateCustomer(CustomerDto customer);
        CustomerDto FindCustomerById(int id);
        void DeleteCustomer(CustomerDto customer);

    }
}
