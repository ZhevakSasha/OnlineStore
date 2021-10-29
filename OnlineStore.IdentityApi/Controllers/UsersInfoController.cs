using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        /// Context field.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Users info controller constructor.
        /// </summary>
        /// <param name="context">Context</param>
        public UsersInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// HttpGet method wich getting all users infom from users db.
        /// </summary>
        /// <returns>Users info</returns>
        [HttpGet]
        [Route("info")]
        public IActionResult GetAllUsers()
        {
            return Ok(_context.Users);
        }

    }
}
