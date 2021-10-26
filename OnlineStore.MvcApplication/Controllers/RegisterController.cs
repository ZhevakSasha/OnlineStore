﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore.MvcApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Controllers
{
    public class RegisterController : Controller
    {
        private readonly string Baseurl = "https://localhost:44301/";
        public ViewResult RegisterForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterForm(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var receivedReservation = new ResponseMessageViewModel();
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    var content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("api/Authenticate/register", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        receivedReservation = JsonConvert.DeserializeObject<ResponseMessageViewModel>(apiResponse);
                        if(receivedReservation.Status == "UserError")
                        {
                            ModelState.AddModelError("Username", receivedReservation.Message);
                            return View();
                        } else 
                        if(receivedReservation.Status == "PasswordError")
                        {
                            ModelState.AddModelError("Password", receivedReservation.Message);
                            return View();
                        }
                    }
                    

                }
                return RedirectToAction("CustomerTable","Customer");
            }
            return View();
        }
    }
}
