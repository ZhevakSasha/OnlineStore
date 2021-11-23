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
    public class UserController : Controller
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
        /// User controller constructor.
        /// </summary>
        /// <param name="factory"></param>
        public UserController(IHttpClientFactory factory, IConfiguration configuration)
        {
            _factory = factory;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Users table view.
        /// </summary>
        /// <returns>View with all registered users</returns>
        public async Task<ActionResult> UsersTable()
        {
            HttpClient client = _factory.CreateClient();
            var receivedReservation = Enumerable.Empty<UserViewModel>();

            client.BaseAddress = new Uri(_configuration.GetValue<string>("Urls:AuthUrl"));
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync("api/UsersInfo/info");
                
            var apiResponse = await response.Content.ReadAsAsync<IEnumerable<UserModel>>();
            receivedReservation = apiResponse.ToList().Select(x => new UserViewModel {Id = x.Id, Username = x.Username, Email = x.Email, Role= string.Join(",",x.Role.OrderBy(f=>f))});
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

            client.BaseAddress = new Uri(_configuration.GetValue<string>("Urls:AuthUrl"));
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync(string.Format("api/UsersInfo/userInfo/{0}", id));
            var apiResponse = await response.Content.ReadAsAsync<UserModel>();

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
        public async Task<ActionResult> UserUpdating(UserModel model, List<string> roles)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _factory.CreateClient();
                model.Role = roles;
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(_configuration.GetValue<string>("Urls:AuthUrl"));
                var accessToken = Request.Cookies["token"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await client.PutAsync("api/UsersInfo/userUpdating", content);

                return RedirectToAction("UsersTable");
            }
            return View();
        }

        /// <summary>
        /// Deletes user info.
        /// </summary>
        /// <returns>Responce</returns>
        public async Task<ActionResult> UserDeleting(string id)
        {
            HttpClient client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");

            client.BaseAddress = new Uri(_configuration.GetValue<string>("Urls:AuthUrl"));
            var accessToken = Request.Cookies["token"];
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.GetAsync(string.Format("api/UsersInfo/userInfo/{0}", id));
            var apiResponse = await response.Content.ReadAsAsync<UserModel>();

            var receivedReservation = apiResponse;

            return View(receivedReservation);
        }

        /// <summary>
        /// Updates user info.
        /// </summary>
        /// <returns>View model with renewable user</returns>
        [HttpPost]
        public async Task<ActionResult> UserDeleting(UserModel model)
        {
                HttpClient client = _factory.CreateClient();

                client.BaseAddress = new Uri(_configuration.GetValue<string>("Urls:AuthUrl"));
                var accessToken = Request.Cookies["token"];
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                await client.DeleteAsync(string.Format("api/UsersInfo/userDeleting/{0}",model.Id));

                return RedirectToAction("UsersTable");
        }
    }   
}
