using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineStore.BusinessLogic.DtoModels;
using OnlineStore.BusinessLogic.IServices;
using OnlineStore.MvcApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Controllers
{
    /// <summary>
    /// Sale controller.
    /// </summary>
    public class SaleController : Controller
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
        /// SaleController constructor.
        /// </summary>
        /// <param name="factory">IHttpClientFactory</param>
        /// <param name="configuration">IConfiguration</param>
        public SaleController(IHttpClientFactory factory, IConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        /// <summary>
        /// Takes a list of all sales from the table and passes them into view.
        /// </summary>
        /// <returns>View with sales</returns>
        public async Task<IActionResult> SaleTable()
        {
            HttpClient client = _factory.CreateClient();
            var receivedReservation = Enumerable.Empty<SaleViewModel>();

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync("serviceApi/Sale/getSales");

            var apiResponse = await response.Content.ReadAsAsync<IEnumerable<SaleModel>>();
            receivedReservation = apiResponse.ToList()
                .Select(x => new SaleViewModel { Id = x.Id, ProductName = x.ProductName, DateOfSale = x.DateOfSale, ProductId =x.ProductId, CustomerId = x.CustomerId, Amount = x.Amount, CustomerName = x.CustomerName });
            return View(receivedReservation);
        }

        /// <summary>
        /// Takes a list of all sales from the table and passes them into view.
        /// </summary>
        /// <returns>View with sales</returns>
        [HttpGet]
        public async Task<IActionResult> SaleTable(string searchString)
        {
            ViewData["GetDetails"] = searchString;

            HttpClient client = _factory.CreateClient();
            var receivedReservation = Enumerable.Empty<SaleViewModel>();

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync("serviceApi/Sale/getSales");

            var apiResponse = await response.Content.ReadAsAsync<IEnumerable<SaleModel>>();
            receivedReservation = apiResponse.ToList()
                .Select(x => new SaleViewModel { Id = x.Id, ProductName = x.ProductName, DateOfSale = x.DateOfSale, ProductId = x.ProductId, CustomerId = x.CustomerId, Amount = x.Amount, CustomerName = x.CustomerName });
            

            if (!String.IsNullOrEmpty(searchString))
            {
                receivedReservation = receivedReservation.Where(s => s.ProductName.Contains(searchString)
                                       || s.CustomerName.Contains(searchString));
            }
            return View(receivedReservation);
        }

        /// <summary>
        /// Takes sale data by id from the table and passes them into view.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>View with sale</returns>
        public async Task<IActionResult> SaleUpdating(int id)
        {
            HttpClient client = _factory.CreateClient();

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync($"serviceApi/Product/getProductsNames");

            var productNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            response = await client.GetAsync($"serviceApi/Customer/getCustomersNames");

            var customerNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            ViewBag.ProductNames = new SelectList(productNames, "Id", "Name");
            ViewBag.CustomerNames = new SelectList(customerNames, "Id", "Name");

            response = await client.GetAsync($"serviceApi/Sale/getSale/{id}");
            var apiResponse = await response.Content.ReadAsAsync<SaleViewModel>();

            var receivedReservation = apiResponse;

            return View(receivedReservation);
        }

        /// <summary>
        ///  Updates sale data.
        /// </summary>
        /// <param name="sale">Takes saleViewModel object</param>
        /// <returns>SaleTable View</returns>
        [HttpPost]
        public async Task<IActionResult> SaleUpdating(SaleViewModel sale)
        {
            HttpClient client = _factory.CreateClient();
            

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(sale), Encoding.UTF8, "application/json");
                var accessToken = Request.Cookies["token"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                await client.PutAsync("serviceApi/Sale/updateSale", content);

                return RedirectToAction("SaleTable");
            }
            
            
            var response = await client.GetAsync($"serviceApi/Product/getProductsNames");

            var productNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            response = await client.GetAsync($"serviceApi/Customer/getCustomersNames");

            var customerNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            ViewBag.ProductNames = new SelectList(productNames, "Id", "Name");
            ViewBag.CustomerNames = new SelectList(customerNames, "Id", "Name");

            return View(sale);
            
        }

        /// <summary>
        /// SaleCreating.
        /// </summary>
        /// <returns>SaleCreating view</returns>
        public async Task<IActionResult> SaleCreating()
        {
            HttpClient client = _factory.CreateClient();

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);

            var response = await client.GetAsync($"serviceApi/Product/getProductsNames");

            var productNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            response = await client.GetAsync($"serviceApi/Customer/getCustomersNames");

            var customerNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            ViewBag.ProductNames = new SelectList(productNames, "Id", "Name");
            ViewBag.CustomerNames = new SelectList(customerNames, "Id", "Name");

            return View();
        }

        /// <summary>
        /// Saves sale data.
        /// </summary>
        /// <param name="sale">Takes saleViewModel object</param>
        /// <returns>SaleTable view</returns>
        [HttpPost]
        public async Task<IActionResult> SaleCreating(SaleViewModel sale)
        {
            HttpClient client = _factory.CreateClient();

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(sale), Encoding.UTF8, "application/json");
                var accessToken = Request.Cookies["token"];

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                await client.PostAsync("serviceApi/Sale/createSale", content);

                return RedirectToAction("SaleTable");
            }
         
            var response = await client.GetAsync("serviceApi/Product/getProductsNames");

            var productNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            response = await client.GetAsync("serviceApi/Customer/getCustomersNames");

            var customerNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            ViewBag.ProductNames = new SelectList(productNames, "Id", "Name");
            ViewBag.CustomerNames = new SelectList(customerNames, "Id", "Name");

            return View(sale);
            
        }

        /// <summary>
        /// Removes sale.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>SaleTable view</returns>
        public async Task<IActionResult> SaleDeleting(int id)
        {
            HttpClient client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync($"serviceApi/Sale/getSale/{id}");
            var apiResponse = await response.Content.ReadAsAsync<SaleViewModel>();

            var receivedReservation = apiResponse;

            return View(receivedReservation);
        }

        /// <summary>
        /// Removes sale.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>SaleTable view</returns>
        [HttpPost]
        public async Task<IActionResult> SaleDeleting(SaleViewModel sale)
        {
            HttpClient client = _factory.CreateClient();

            client.BaseAddress = new Uri(_configuration.GetValue<string>("Urls:ServiceUrl"));
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            await client.DeleteAsync(string.Format("serviceApi/Sale/deleteSale/{0}", sale.Id));

            return RedirectToAction("SaleTable");
        }
    }
}
