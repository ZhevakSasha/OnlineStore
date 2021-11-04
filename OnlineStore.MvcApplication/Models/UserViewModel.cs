using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Property  for storing email.
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Property  for storing user role.
        /// </summary>
        [Display(Name = "Roles")]
        public string Role { get; set; }
    }
}