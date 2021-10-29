using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    public class UserViewModel 
    {
        /// <summary>
        /// Property  for storing username.
        /// </summary>
        [JsonProperty("userName")]
        public string Username { get; set; }

        /// <summary>
        /// Property  for storing email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

    }
}
