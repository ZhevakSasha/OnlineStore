using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ServiceApi.Controllers
{
    [ApiController]
    [Route("serviceApi/[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

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

        [HttpGet("id", Name = "getCustomer")]
        [Authorize(Roles = "Admin")]
        public ActionResult<CustomerDto> GetCustomerById(int id)
        {
            var customer = _customerService.FindCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [Route("createCustomer")]
        public ActionResult<CustomerDto> CreateCustomer(CustomerDto customer)
        {
            _customerService.CreateCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { Id = customer.Id }, customer);
        }

        [HttpPut]
        [Route("updateCustomer")]
        public ActionResult UpdateCustomer(CustomerDto customerService)
        {
            var customer = _customerService.FindCustomerById(customerService.Id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerService.UpdateCustomer(customer);

            return NoContent();
        }

        [HttpDelete]
        [Route("deleteCustomer")]
        public ActionResult DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
            return NoContent();
        } 
    }
}
