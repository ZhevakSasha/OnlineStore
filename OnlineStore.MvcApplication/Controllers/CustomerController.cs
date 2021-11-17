using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using OnlineStore.MvcApplication.Models;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace OnlineStore.MvcApplication.Controllers
{
    /// <summary>
    /// Customer controller.
    /// </summary>
    public class CustomerController : Controller
    {
        /// <summary>
        /// IHttpClientFactory.
        /// </summary>
        private readonly IHttpClientFactory _factory;

        /// <summary>
        /// IConfiguration field.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// CustomerController constructor.
        /// </summary>
        /// <param name="factory">IHttpClientFactory</param>
        /// <param name="configuration">IConfiguration</param>
        public CustomerController(IHttpClientFactory factory, IConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        /// <summary>
        /// Takes a list of all customers from the table and passes them into view.
        /// </summary>
        /// <returns>View with customers</returns>
        public async Task<ActionResult> CustomerTable()
        {
            HttpClient client = _factory.CreateClient();
            var receivedReservation = Enumerable.Empty<CustomerViewModel>();

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync("serviceApi/Customer/getCustomers");

            var apiResponse = await response.Content.ReadAsAsync<IEnumerable<CustomerModel>>();
            receivedReservation = apiResponse.ToList().Select(x => new CustomerViewModel { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Address = x.Address, PhoneNumber= x.PhoneNumber});
            return View(receivedReservation);
        }

        /// <summary>
        /// Takes customer data by id from the table and passes them into view.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>View with customer</returns>
        [HttpGet]
        public async Task<IActionResult> CustomerUpdating(int id)
        {
            HttpClient client = _factory.CreateClient();

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync($"serviceApi/Customer/getCustomer/{id}");
            var apiResponse = await response.Content.ReadAsAsync<CustomerViewModel>();

            var receivedReservation = apiResponse;

            return View(receivedReservation);
        }

        /// <summary>
        /// Updates customer data.
        /// </summary>
        /// <param name="customer">Takes customerViewModel object</param>
        /// <returns>CustomerTable view</returns>
        [HttpPost]
        public async Task<IActionResult> CustomerUpdating(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _factory.CreateClient();
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
                var accessToken = Request.Cookies["token"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                await client.PutAsync("serviceApi/Customer/updateCustomer", content);

                return RedirectToAction("CustomerTable");
            }
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
        public async Task<IActionResult> CustomerCreating(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _factory.CreateClient();
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
                var accessToken = Request.Cookies["token"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                await client.PostAsync("serviceApi/Customer/createCustomer", content);

                return RedirectToAction("CustomerTable");
            }
            return View();
        }

        /// <summary>
        /// Removes customer.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>CustomerTable view</returns>
        public async Task<IActionResult> CustomerDeleting(int id)
        {
            HttpClient client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync($"serviceApi/Customer/getCustomer/{id}");
            var apiResponse = await response.Content.ReadAsAsync<CustomerViewModel>();

            var receivedReservation = apiResponse;

            return View(receivedReservation);
        }

        /// <summary>
        /// Removes customer.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>CustomerTable view</returns>
        [HttpPost]
        public async Task<IActionResult> CustomerDeleting(CustomerViewModel customer)
        {
            HttpClient client = _factory.CreateClient();

            client.BaseAddress = new Uri(_configuration.GetValue<string>("Urls:ServiceUrl"));
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            await client.DeleteAsync(string.Format("serviceApi/Customer/deleteCustomer/{0}", customer.Id));

            return RedirectToAction("CustomerTable");
        }
    }
}
