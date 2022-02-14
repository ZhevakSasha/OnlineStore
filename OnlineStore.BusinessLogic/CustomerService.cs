using AutoMapper;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess;
using OnlineStore.DataAccess.PagedList;
using OnlineStore.Domain.Models;
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
        private UnitOfWork _unitOfWork;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// CustomerService constructor.
        /// </summary>
        /// <param name="customer">Customer repository</param>
        public CustomerService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// GetAllCustomers method.
        /// </summary>
        /// <returns>All customerDto objects from table</returns>
        public PagedList<CustomerDto> GetAllCustomers(PageParameters pageParameters)
        {
            var customers = _unitOfWork.Customers.GetList(pageParameters);
            var count = customers.TotalCount;

            return new PagedList<CustomerDto>(_mapper.Map<List<CustomerDto>>(customers), 
                count, 
                pageParameters.PageNumber,
                pageParameters.PageSize);
        }

        /// <summary>
        /// CreateCustomer method.
        /// </summary>
        /// <param name="customerModel">Takes customerDto object</param>
        public void CreateCustomer(CustomerDto customerModel)
        {
            var customer = _mapper.Map<Customer>(customerModel);
            _unitOfWork.Customers.Create(customer);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Update customer method.
        /// </summary>
        /// <param name="customerModel">Takes customerDto object</param>
        public void UpdateCustomer(CustomerDto customerModel)
        {
            var customer = _mapper.Map<Customer>(customerModel);
            _unitOfWork.Customers.Update(customer);
            _unitOfWork.Save();
        }

        /// <summary>
        /// FindCustomerById method.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>CustomerDto object by id</returns>
        public CustomerDto FindCustomerById(int id)
        {
            var customer = _unitOfWork.Customers.GetEntity(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public CustomerSaleReportDto ReportByCustomer(int id)
        {
            var customer = _unitOfWork.Customers.GetEntity(id);
            foreach (var sale in customer.Sales)
            {
                sale.Products = _unitOfWork.Sales.GetEntity(sale.Id).Products;
            }

            return _mapper.Map<CustomerSaleReportDto>(customer);
        }


        /// <summary>
        /// GetAllCustomerNames method.
        /// </summary>
        /// <returns>IEnumerable<SelectDto></returns>
        public IList<SelectDto> GetAllCustomerNames()
        {
            var customerNames = _unitOfWork.Customers
                .GetList()
                .Select(s => new SelectDto
                {
                    Id = s.Id,
                    Name = $"{s.FirstName.Substring(0, 1)}. {s.LastName}"
                }).ToList();
            return customerNames;
        }

        /// <summary>
        /// Delete customer method.
        /// </summary>
        /// <param name="id">id</param>
        public void DeleteCustomer(int id)
        {
            _unitOfWork.Customers.Delete(id);
            _unitOfWork.Save();
        }
    }
}
