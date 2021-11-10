using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.IdentityApi.Controllers
{
    /// <summary>
    /// Users info controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersInfoController : ControllerBase
    {
        /// <summary>
        /// User manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Role manager.
        /// </summary>
        private readonly RoleManager<IdentityRole> roleManager;


        /// <summary>
        /// Users info controller constructor.
        /// </summary>
        /// <param name="context">Context</param>
        public UsersInfoController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// HttpGet method wich getting all users infom from users db.
        /// </summary>
        /// <returns>Users info</returns>
        [HttpGet]
        [Route("info")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserModel>> GetAllUsers()
        {
            var users = userManager.Users.ToList();

            if (!users.Any())
            {
                return NotFound();
            }

            var usersDisplay = users.Select(user => new UserModel
            {
                Id = user.Id,
                Username = user.UserName,
                Roles = userManager.GetRolesAsync(user).Result,
                Email = user.Email
            }).ToList();

            return Ok(usersDisplay);
        }

        /// <summary>
        /// HttpGet method getting user vy id.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User model</returns>
        [HttpGet]
        [Route("userInfo/{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);
            var model = new UserModel { Id = user.Id, Email = user.Email, Roles = roles, Username = user.UserName};
            return Ok(model);
        }

        /// <summary>
        /// HttpGet method wich getting all roles infom from roles db.
        /// </summary>
        /// <returns>Users info</returns>
        [HttpGet]
        [Route("rolesInfo")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<string>> GetAllRoles()
        {
            var roles = roleManager.Roles.ToList().Select(x => x.Name);
            return Ok(roles);
        }

        /// <summary>
        /// HttpPost method updates user info.
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>Responce</returns>
        [HttpPost]
        [Route("userUpdating")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserUpdating([FromBody]UserModel model)
        {
           var user = await userManager.FindByIdAsync(model.Id);
           user.Email = model.Email;
           user.UserName = model.Username;
           await userManager.AddToRoleAsync(user, model.Roles.FirstOrDefault());
           //await userManager.UpdateAsync(user);
           return Ok(new Response { Status = "Success", Message = "User updated successfully!" });
        }
    }
}
