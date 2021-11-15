using System.ComponentModel.DataAnnotations;

namespace OnlineStore.IdentityApi
{
    public class RegisterModel
    {
        /// <summary>
        /// Property for storing username.
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        /// <summary>
        /// Property for storing email.
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /// <summary>
        /// Property for storing password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        /// <summary>
        /// Property for storing petname.
        /// </summary>
        [Required(ErrorMessage = "PetName is required")]
        public string PetName { get; set; }
    }
}
