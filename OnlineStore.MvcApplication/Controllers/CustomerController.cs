using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
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
        /// <param name="mapper">Mapper</param>
        public CustomerController(ICustomerService customer, IMapper mapper)
        {
            _customer = customer;
            _mapper = mapper;
        }

        /// <summary>
        /// Takes a list of all customers from the table and passes them into view.
        /// </summary>
        /// <returns>View with customers</returns>
        public IActionResult CustomerTable()
        {
            var results = _customer.GetAllCustomers();
            var customers = _mapper.Map<IEnumerable<CustomerViewModel>>(results);
            return View(customers);
        }

        /// <summary>
        /// Takes customer data by id from the table and passes them into view.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>View with customer</returns>
        public IActionResult CustomerUpdating(int id)
        {
            var customer = _customer.FindCustomerById(id);
            return View(_mapper.Map<CustomerViewModel>(customer));
        }

        /// <summary>
        /// Updates customer data.
        /// </summary>
        /// <param name="customer">Takes customerViewModel object</param>
        /// <returns>CustomerTable view</returns>
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
        
        /// <summary>
        /// CustomerCreating.
        /// </summary>
        /// <returns>CustomerCreating view.</returns>
        public IActionResult CustomerCreating()
        {
            return View();
        }

        /// <summary>
        /// Saves customer data.
        /// </summary>
        /// <param name="customer">Takes customerViewModel object</param>
        /// <returns>CustomerTable view</returns>
        [HttpPost]
        public IActionResult CustomerCreating(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                _customer.CreateCustomer(_mapper.Map<CustomerDto>(customer));
                return RedirectToAction("CustomerTable");
            } 
            else
                return View();
        }

        /// <summary>
        /// Removes customer.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>CustomerTable view</returns>
        public IActionResult CustomerDeleting(int id)
        {
            var customer = _mapper.Map<CustomerViewModel>(_customer.FindCustomerById(id));
            return View(customer);
        }

        /// <summary>
        /// Removes customer.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>CustomerTable view</returns>
        [HttpPost]
        public IActionResult CustomerDeleting(CustomerViewModel customer)
        {
            _customer.DeleteCustomer(customer.Id);
            return RedirectToAction("CustomerTable");
        }
    }
}
