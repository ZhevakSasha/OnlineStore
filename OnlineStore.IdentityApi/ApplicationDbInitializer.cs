using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.IdentityApi
{
    public static class ApplicationDbInitializer
    {
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
