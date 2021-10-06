using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using OnlineStore.DataAccess.RepositoryPatterns;
using System.Collections.Generic;
using System.Linq;

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
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// CustomerService constructor.
        /// </summary>
        /// <param name="customer">Customer repository</param>
        public CustomerService(ICustomerRepository customer, IMapper mapper)
        {
            _customer = customer;
            _mapper = mapper;
        }

        /// <summary>
        /// GetAllCustomers method.
        /// </summary>
        /// <returns>All customerDto objects from table</returns>
        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            var customers = _customer.GetList();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        /// <summary>
        /// CreateCustomer method.
        /// </summary>
        /// <param name="customerModel">Takes customerDto object</param>
        public void CreateCustomer(CustomerDto customerModel)
        {
            var customer = _mapper.Map<Customer>(customerModel);
            _customer.Create(customer);
            _customer.Save();
        }

        /// <summary>
        /// Update customer method.
        /// </summary>
        /// <param name="customerModel">Takes customerDto object</param>
        public void UpdateCustomer(CustomerDto customerModel)
        {
            var customer = _mapper.Map<Customer>(customerModel);
            _customer.Update(customer);
            _customer.Save();
        }

        /// <summary>
        /// FindCustomerById method.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>CustomerDto object by id</returns>
        public CustomerDto FindCustomerById(int id)
        {
            var customer = _customer.GetEntity(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        /// <summary>
        /// GetAllCustomerNames method.
        /// </summary>
        /// <returns>IEnumerable<SelectDto></returns>
        public IEnumerable<SelectDto> GetAllCustomerNames()
        {
            var customerNames = _customer
                .GetList()
                .Select(s => new SelectDto
                {
                    Id = s.Id,
                    Name = $"{s.FirstName.Substring(0, 1)}." +
                $" {s.LastName}"
                }
                    ) ;
            return customerNames;
        }

        /// <summary>
        /// Delete customer method.
        /// </summary>
        /// <param name="id">id</param>
        public void DeleteCustomer(int id)
        {
            _customer.Delete(id);
            _customer.Save();
        }
    }
}
