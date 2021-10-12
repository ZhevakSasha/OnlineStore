using OnlineStore.BusinessLogic.DtoModels;
using System.Collections.Generic;

namespace OnlineStore.BusinessLogic.IServices
{
    /// <summary>
    /// CustomerService interface.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// GetAllCustomers method.
        /// </summary>
        /// <returns>Returns all customers from table</returns>
        IEnumerable<CustomerDto> GetAllCustomers();

        /// <summary>
        /// CreateCustomer method. 
        /// </summary>
        /// <param name="customer">Takes CustomerDto object</param>
        void CreateCustomer(CustomerDto customer);

        /// <summary>
        /// Update customer method.
        /// </summary>
        /// <param name="customer">Takes CustomerDto object</param>
        void UpdateCustomer(CustomerDto customer);

        /// <summary>
        /// FindCustomerById method.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>CustomerDto object</returns>
        CustomerDto FindCustomerById(int id);

        /// <summary>
        /// DeleteCustomer method deletes customer by id.
        /// </summary>
        /// <param name="id">id</param>
        void DeleteCustomer(int id);

        /// <summary>
        /// GetAllCustomerNames method.
        /// </summary>
        /// <returns>Returns all customer names from cusomer table</returns>
        IEnumerable<SelectDto> GetAllCustomerNames();



    }
}
