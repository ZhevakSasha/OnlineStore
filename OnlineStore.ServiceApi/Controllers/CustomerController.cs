using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using System.Collections.Generic;

namespace OnlineStore.ServiceApi.Controllers
{
    /// <summary>
    /// ServiceApi CustomerController.
    /// </summary>
    [ApiController]
    [Route("serviceApi/[controller]")]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Customer service field.
        /// </summary>
        private ICustomerService _customerService;

        /// <summary>
        /// CustomerController constructor.
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// HttpGet endpoint with all customers.
        /// </summary>
        /// <returns>List with all customers</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("getCustomers")]
        public ActionResult<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        /// <summary>
        /// HttpGet endpoint with all customers names.
        /// </summary>
        /// <returns>List with all customers names</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("getCustomersNames")]
        public ActionResult<IEnumerable<SelectDto>> GetAllCustomersNames()
        {
            var customersNames = _customerService.GetAllCustomerNames();

            if (customersNames == null)
            {
                return NotFound();
            }

            return Ok(customersNames);
        }
        
        /// <summary>
        /// HttpGet endpoint with customer by id.
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Customer</returns>
        [HttpGet]
        [Route("getCustomer/{id}")]
        public ActionResult<CustomerDto> GetCustomerById(int id)
        {
            var customer = _customerService.FindCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        /// <summary>
        /// HttpPost endpoint. Creates customer.
        /// </summary>
        /// <param name="customer">Customer</param>
        [HttpPost]
        [Route("createCustomer")]
        [Authorize(Roles ="Admin")]
        public ActionResult<CustomerDto> CreateCustomer([FromBody]CustomerDto customer)
        {
            _customerService.CreateCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { Id = customer.Id }, customer);
        }

        /// <summary>
        /// HttpPut endpoint. Updates customer.
        /// </summary>
        /// <param name="customerService">Customer</param>
        [HttpPut]
        [Route("updateCustomer")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateCustomer([FromBody] CustomerDto customerService)
        {
            var customer = _customerService.FindCustomerById(customerService.Id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Id = customerService.Id;
            customer.FirstName = customerService.FirstName;
            customer.LastName = customerService.LastName;
            customer.Address = customerService.Address;
            customer.PhoneNumber = customerService.PhoneNumber;

            _customerService.UpdateCustomer(customer);

            return NoContent();
        }


        /// <summary>
        /// HttpDelete endpoint. Deletes customer.
        /// </summary>
        /// <param name="id">Customer id</param>
        [HttpDelete]
        [Route("deleteCustomer/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
            return NoContent();
        } 
    }
}
