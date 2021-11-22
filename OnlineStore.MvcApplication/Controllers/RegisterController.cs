using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineStore.MvcApplication.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Controllers
{
    /// <summary>
    /// Register controller.
    /// </summary>
    public class RegisterController : Controller
    {
        private HttpClient client;

        /// <summary>
        /// Register controller.
        /// </summary>
        /// <param name="factory"></param>
        public RegisterController(IHttpClientFactory factory)
        {
            client = factory.CreateClient("authApi");
        }

        /// <summary>
        /// Register form.
        /// </summary>
        /// <returns>View with register form</returns>
        public ViewResult RegisterForm()
        {
            return View();
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="registerModel">User register model</param>
        /// <returns>RedirectToAction</returns>
        [HttpPost]
        public async Task<ActionResult> RegisterForm(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var receivedReservation = new ResponseMessageViewModel();
                client.DefaultRequestHeaders.Clear();
                var content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Authenticate/register", content);

                string apiResponse = await response.Content.ReadAsStringAsync();
                receivedReservation = JsonConvert.DeserializeObject<ResponseMessageViewModel>(apiResponse);
                if (receivedReservation.Status == "UserError")
                {
                    ModelState.AddModelError("Username", receivedReservation.Message);
                    return View();
                }
                else
                if (receivedReservation.Status == "PasswordError")
                {
                    ModelState.AddModelError("Password", receivedReservation.Message);
                    return View();
                }
                return RedirectToAction("CustomerTable","Customer");
            }
            return View();
        }
    }
}
