using System.Collections.Generic;

namespace OnlineStore.IdentityApi
{
    /// <summary>
    /// Respnonse class.
    /// </summary>
    public class UserModel
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
        public IList<string> Roles { get; set; }
    }
}
