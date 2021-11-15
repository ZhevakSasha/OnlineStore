using System.ComponentModel.DataAnnotations;

namespace OnlineStore.IdentityApi
{
    /// <summary>
    /// Login model.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Property for storing username.
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        /// <summary>
        /// Property for storing password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
