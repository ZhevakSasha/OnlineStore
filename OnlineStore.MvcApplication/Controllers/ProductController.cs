using Microsoft.AspNetCore.Mvc;
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
    /// Product controller.
    /// </summary>
    public class ProductController : Controller
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
        /// ProductController constructor.
        /// </summary>
        /// <param name="factory">IHttpClientFactory</param>
        /// <param name="configuration">IConfiguration</param>
        public ProductController(IHttpClientFactory factory, IConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }

        /// <summary>
        /// Takes a list of all products from the table and passes them into view.
        /// </summary>
        /// <returns>View with products</returns>
        public async Task<IActionResult> ProductTable()
        {
            HttpClient client = _factory.CreateClient();
            var receivedReservation = Enumerable.Empty<ProductViewModel>();

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync("serviceApi/Product/getProducts");

            var apiResponse = await response.Content.ReadAsAsync<IEnumerable<ProductModel>>();
            receivedReservation = apiResponse.ToList().Select(x => new ProductViewModel { Id = x.Id, ProductName = x.ProductName, Price = x.Price, UnitOfMeasurement = x.UnitOfMeasurement });
            return View(receivedReservation);
        }

        /// <summary>
        /// Takes product data by id from the table and passes them into view.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>View with product</returns>
        public async Task<IActionResult> ProductUpdating(int id)
        {
            HttpClient client = _factory.CreateClient();

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync($"serviceApi/Product/getProduct/{id}");
            var apiResponse = await response.Content.ReadAsAsync<ProductViewModel>();

            var receivedReservation = apiResponse;

            return View(receivedReservation);
        }

        /// <summary>
        /// Updates product data.
        /// </summary>
        /// <param name="product">Takes productViewModel object</param>
        /// <returns>ProductTable view</returns>
        [HttpPost]
        public async Task<IActionResult> ProductUpdating(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _factory.CreateClient();
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
                var accessToken = Request.Cookies["token"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var responseMessage = await client.PutAsync("serviceApi/Product/updateProduct", content);

                return RedirectToAction("ProductTable");
            }
            return View();
        }

        /// <summary>
        /// ProductCreating.
        /// </summary>
        /// <returns>ProductCreating View</returns>
        public IActionResult ProductCreating()
        {
            return View();
        }

        /// <summary>
        /// Saves product data.
        /// </summary>
        /// <param name="product">Takes productViewModel object</param>
        /// <returns>Table view</returns>
        [HttpPost]
        public async Task<IActionResult> ProductCreating(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _factory.CreateClient();
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
                var accessToken = Request.Cookies["token"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                await client.PostAsync("serviceApi/Product/createProduct", content);

                return RedirectToAction("ProductTable");
            }
            return View();
        }

        /// <summary>
        /// Removes product.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ProductTable view</returns>
        public async Task<IActionResult> ProductDeleting(int id)
        {
            HttpClient client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");

            client.BaseAddress = new Uri(_configuration.GetSection("Urls:ServiceUrl").Value);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync($"serviceApi/Product/getProduct/{id}");
            var apiResponse = await response.Content.ReadAsAsync<ProductViewModel>();

            var receivedReservation = apiResponse;

            return View(receivedReservation);
        }

        /// <summary>
        /// Removes product.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ProductTable view</returns>
        [HttpPost]
        public async Task<IActionResult> ProductDeleting(ProductViewModel product)
        {
            HttpClient client = _factory.CreateClient();

            client.BaseAddress = new Uri(_configuration.GetValue<string>("Urls:ServiceUrl"));
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            await client.DeleteAsync(string.Format("serviceApi/Product/deleteProduct/{0}", product.Id));

            return RedirectToAction("ProductTable");
        }
    }
}
