using Microsoft.AspNetCore.Identity;

namespace OnlineStore.IdentityApi
{
    /// <summary>
    /// ApplicationUser model.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string PetName { get; set; }
    }
}
