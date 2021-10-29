using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace OnlineStore.IdentityApi
{
    /// <summary>
    /// ApplicationDbInitializer.
    /// </summary>
    public static class ApplicationDbInitializer
    {
        /// <summary>
        /// SeedUsers method. Seeding default user and adding to him admin role.
        /// </summary>
        /// <param name="userManager">userManager</param>
        /// <returns></returns>
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.FindByNameAsync("Admin");

            if (admin != null)
            {
                return;
            }

            var user = new ApplicationUser
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false
            };

            var result = await userManager.CreateAsync(user, "Admin*123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
