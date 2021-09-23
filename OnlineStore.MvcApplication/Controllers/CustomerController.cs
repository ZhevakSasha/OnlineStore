using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic;
using OnlineStore.DataAccess.DataAccess;
using OnlineStore.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerLogic _customer;

        private DataBaseContext _context;

        public CustomerController(DataBaseContext context)
        {
            _context = context;
            _customer = new CustomerLogic(_context);
        }
        public IActionResult CustomerTable()
        {
            var results = _customer.GetAllCustomers();
            return View(results);
        }

        public IActionResult CustomerUpdating(int id)
        {
            Customer customer = _customer.FindCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult CustomerUpdating(Customer customer)
        {
            _customer.UpdateCustomer(customer);
            return RedirectToAction("CustomerTable");
        }

        public IActionResult CustomerCreating()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerCreating(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customer.CreateCustomer(customer);
                return RedirectToAction("CustomerTable");
            } else
            return View();
        }

        public IActionResult CustomerDeleting(int id)
        {
            Customer customer = _customer.FindCustomerById(id);
            _customer.DeleteCustomer(customer);
            return RedirectToAction("CustomerTable");
        }

    }
}
