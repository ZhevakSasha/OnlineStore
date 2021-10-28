using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class LoginController : Controller
    {
        private readonly string Baseurl = "https://localhost:44301/";

        public ViewResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginForm(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var receivedReservation = new ResponceViewModel();
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("api/Authenticate/login", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        
                        receivedReservation = JsonConvert.DeserializeObject<ResponceViewModel>(apiResponse);  
                        if(receivedReservation.Status == 401)
                        {
                            ViewBag.ErrorMessage = "Unauthorized! Username or password incorrect!";
                            return View();
                        }
                        CookieOptions option = new CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(15)
                        };
                        Response.Cookies.Append("token",  receivedReservation.Token, option);
                    }
                }

                return RedirectToAction("CustomerTable", "Customer");
            }
            return View();
        }
    }
    
}
