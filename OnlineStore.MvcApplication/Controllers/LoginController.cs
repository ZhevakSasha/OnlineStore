using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore.MvcApplication.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Controllers
{
    /// <summary>
    /// Login controller.
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Base url of api.
        /// </summary>
        private readonly string Baseurl = "https://localhost:44301/";

        /// <summary>
        /// Login form.
        /// </summary>
        /// <returns>View with login form</returns>
        public ViewResult LoginForm()
        {
            return View();
        }

        /// <summary>
        /// Authorize user and saving JwtToken.
        /// </summary>
        /// <param name="loginModel">User login model</param>
        /// <returns>RedirectToAction</returns>
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

                AuthorizeHandle(receivedReservation.Token);

                return RedirectToAction("CustomerTable", "Customer");
            }
            return View();
        }

        /// <summary>
        /// AuthorizeHandle. Decode user token and authenticate them.
        /// </summary>
        /// <param name="token">JwtToken</param>
        private async void AuthorizeHandle(string token)
        {
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            ClaimsIdentity id = new ClaimsIdentity(decodedToken.Claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        /// <summary>
        /// Logout method. SignOut user.
        /// </summary>
        /// <returns>RedirectToAction</returns>
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("token");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginForm", "Login");
        }
    }
}
