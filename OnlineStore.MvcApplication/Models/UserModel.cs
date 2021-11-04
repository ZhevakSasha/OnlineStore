using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public string Username { get; set; }

        /// <summary>
        /// Property  for storing email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Property  for storing user role.
        /// </summary>
        [JsonProperty("roles")]
        public IList<string> Role { get; set; }
    }
}
