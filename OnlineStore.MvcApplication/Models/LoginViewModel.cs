using System.ComponentModel.DataAnnotations;

namespace OnlineStore.MvcApplication.Models
{
    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Property  for storing username.
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Property  for storing password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
