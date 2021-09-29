using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
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

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            var customers = _customer.GetList();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public void CreateCustomer(CustomerDto customerModel)
        {
            var customer = _mapper.Map<Customer>(customerModel);
            _customer.Create(customer);
            _customer.Save();
        }

        public void UpdateCustomer(CustomerDto customerModel)
        {
            var customer = _mapper.Map<Customer>(customerModel);
            _customer.Update(customer);
            _customer.Save();
        }

        public CustomerDto FindCustomerById(int id)
        {
            var customer = _customer.GetEntity(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public void DeleteCustomer(CustomerDto customerModel)
        {
            var customer = _mapper.Map<Customer>(customerModel);
            _customer.Delete(customer);
            _customer.Save();
        }
    }
}
