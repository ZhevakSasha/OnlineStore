using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication.Models
{
    /// <summary>
    /// UserViewModel for displaying users on view.
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Property  for storing user id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Property  for storing username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Property  for storing email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Property  for storing user role.
        /// </summary>
        public string Role { get; set; }
    }
}