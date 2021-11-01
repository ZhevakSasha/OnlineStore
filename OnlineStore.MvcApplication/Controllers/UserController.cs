using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore.MvcApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _factory;

        private readonly string Baseurl = "https://localhost:44301/";

        public UserController(IHttpClientFactory factory)
        {
            _factory = factory;
        }
       
        public async Task<ActionResult> UsersTable()
        {
            HttpClient client = _factory.CreateClient();
            var receivedReservation = Enumerable.Empty<UserViewModel>();

            client.BaseAddress = new Uri(Baseurl);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync("api/UsersInfo/info");
                
            var apiResponse = await response.Content.ReadAsAsync<IEnumerable<UserViewModel>>();
            receivedReservation = apiResponse;

            return View(receivedReservation);
        }
    }
}
