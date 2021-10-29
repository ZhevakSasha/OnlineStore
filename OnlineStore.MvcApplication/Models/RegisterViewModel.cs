using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MvcApplication.Models
{
    /// <summary>
    /// Register view model.
    /// </summary>
    public class RegisterViewModel
    {
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
        /// Property  for storing password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
