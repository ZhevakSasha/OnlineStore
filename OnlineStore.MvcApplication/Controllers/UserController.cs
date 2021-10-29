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

        private readonly string Baseurl = "https://localhost:44301/";

        public async Task<ActionResult> UsersTable()
        {
            var receivedReservation = Enumerable.Empty<UserViewModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();

                using (var response = await httpClient.GetAsync("api/UsersInfo/info"))
                {
                    var apiResponse = await response.Content.ReadAsAsync<IEnumerable<UserViewModel>>();
                    receivedReservation = apiResponse;
                }
            }

            return View(receivedReservation);
        }
    }
}
