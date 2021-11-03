using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class UserController : Controller
    {
        /// <summary>
        /// IHttpClientFactory.
        /// </summary>
        private readonly IHttpClientFactory _factory;

        /// <summary>
        /// Base url of api.
        /// </summary>
        private readonly string Baseurl = "https://localhost:44301/";

        /// <summary>
        /// User controller constructor.
        /// </summary>
        /// <param name="factory"></param>
        public UserController(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        
        /// <summary>
        /// Users table view.
        /// </summary>
        /// <returns>View with all registered users</returns>
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

        /// <summary>
        /// Updates user info.
        /// </summary>
        /// <returns>View model with renewable user</returns>
        public async Task<ActionResult> UserUpdating(string id)
        {
            HttpClient client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");

            client.BaseAddress = new Uri(Baseurl);
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync(string.Format("api/UsersInfo/userInfo/{0}", id));
            var apiResponse = await response.Content.ReadAsAsync<UserViewModel>();

            var roles = await client.GetAsync("api/UsersInfo/rolesInfo");
            var apiRolesResponse = await roles.Content.ReadAsAsync<List<String>>();
            ViewBag.RoleNames = new SelectList(apiRolesResponse);

            var receivedReservation = apiResponse;

            return View(receivedReservation);
        }

        /// <summary>
        /// Updates user info.
        /// </summary>
        /// <returns>View model with renewable user</returns>
        [HttpPost]
        public async Task<ActionResult> UserUpdating(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _factory.CreateClient();
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(Baseurl);
                var accessToken = Request.Cookies["token"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await client.PostAsync("api/UsersInfo/userUpdating", content);

                return RedirectToAction("UsersTable");
            }
            return View();
        }
    }   
}
