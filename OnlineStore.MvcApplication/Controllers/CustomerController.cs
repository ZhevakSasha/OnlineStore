using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.DataAccess.DataModel;

namespace OnlineStore.MvcApplication.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService _customer;


        public CustomerController(ICustomerService customer)
        {
            _customer = customer;
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
