using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
