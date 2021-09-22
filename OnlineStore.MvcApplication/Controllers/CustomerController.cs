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

        public IActionResult CustomerTable()
        {
            return View(_context.Customers);
        }

        public CustomerController(DataBaseContext context)
        {
            _context = context;
            _customer = new CustomerLogic(_context);
        }

        [HttpGet]
        [Route("getCustomers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customer.GetAllCustomers();
        }
    }
}
