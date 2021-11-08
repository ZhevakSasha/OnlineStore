using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    public class UserModel
    {
        /// <summary>
        /// Property  for storing user id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Property  for storing username.
        /// </summary>
        [JsonProperty("username")]
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Property  for storing email.
        /// </summary>
        [JsonProperty("email")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Property  for storing user role.
        /// </summary>
        [JsonProperty("roles")]
        [Display(Name = "Role")]
        public IList<string> Role { get; set; }
    }
}
