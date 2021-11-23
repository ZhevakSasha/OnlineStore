using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        private HttpClient client;

        /// <summary>
        /// SaleController constructor.
        /// </summary>
        /// <param name="factory">IHttpClientFactory</param>
        /// <param name="configuration">IConfiguration</param>
        public SaleController(IHttpClientFactory factory)
        {
            client = factory.CreateClient("serviceApi");
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
            var receivedReservation = Enumerable.Empty<SaleViewModel>();

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

            var receivedReservation = Enumerable.Empty<SaleViewModel>();

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
            var response = await client.GetAsync($"serviceApi/Product/getProductsNames");

            var productNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            response = await client.GetAsync($"serviceApi/Customer/getCustomersNames");

            var customerNames = await response.Content.ReadAsAsync<IEnumerable<SelectModel>>();

            ViewBag.ProductNames = new SelectList(productNames, "Id", "Name");
            ViewBag.CustomerNames = new SelectList(customerNames, "Id", "Name");

            response = await client.GetAsync($"serviceApi/Sale/getSale/{id}");
            var apiResponse = await response.Content.ReadAsAsync<SaleViewModel>();

            var receivedReservation = apiResponse;

            if (response.IsSuccessStatusCode)
                return View(receivedReservation);

            return RedirectToAction("LoginForm", "Login");
        }

        /// <summary>
        ///  Updates sale data.
        /// </summary>
        /// <param name="sale">Takes saleViewModel object</param>
        /// <returns>SaleTable View</returns>
        [HttpPost]
        public async Task<IActionResult> SaleUpdating(SaleViewModel sale)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(sale), Encoding.UTF8, "application/json");

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
            if (ModelState.IsValid)
            {
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
            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");

            var response = await client.GetAsync($"serviceApi/Sale/getSale/{id}");


            var apiResponse = await response.Content.ReadAsAsync<SaleViewModel>();

            var receivedReservation = apiResponse;

            if (response.IsSuccessStatusCode)
                return View(receivedReservation);

            return RedirectToAction("LoginForm", "Login");
        }

        /// <summary>
        /// Removes sale.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>SaleTable view</returns>
        [HttpPost]
        public async Task<IActionResult> SaleDeleting(SaleViewModel sale)
        {
            await client.DeleteAsync(string.Format("serviceApi/Sale/deleteSale/{0}", sale.Id));

            return RedirectToAction("SaleTable");
        }
    }
}
