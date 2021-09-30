using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;
using AutoMapper;
using OnlineStore.MvcApplication.Models;
using System.Collections.Generic;

namespace OnlineStore.MvcApplication.Controllers
{
    /// <summary>
    /// Customer controller.
    /// </summary>
    public class CustomerController : Controller
    {

        /// <summary>
        /// Customer service.
        /// </summary>
        private ICustomerService _customer;

        /// <summary>
        /// Mapper.
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// CustomerController constructor.
        /// </summary>
        /// <param name="customer">Customer service</param>
        public CustomerController(ICustomerService customer, IMapper mapper)
        {
            _customer = customer;
            _mapper = mapper;
        }

        public IActionResult CustomerTable()
        {
            var results = _customer.GetAllCustomers();
            var customers = _mapper.Map<IEnumerable<CustomerViewModel>>(results);
            return View(customers);
        }

        public IActionResult CustomerUpdating(int id)
        {
            var customer = _customer.FindCustomerById(id);
            return View(_mapper.Map<CustomerViewModel>(customer));
        }

        [HttpPost]
        public IActionResult CustomerUpdating(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                _customer.UpdateCustomer(_mapper.Map<CustomerDto>(customer));
                return RedirectToAction("CustomerTable");
            }
            else
                return View();

        }
        
        public IActionResult CustomerCreating()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerCreating(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                _customer.CreateCustomer(_mapper.Map<CustomerDto>(customer));
                return RedirectToAction("CustomerTable");
            } else
            return View();
        }

        public IActionResult CustomerDeleting(int id)
        {
            var customer = _customer.FindCustomerById(id);
            _customer.DeleteCustomer(customer);
            return RedirectToAction("CustomerTable");
        }
    }
}
