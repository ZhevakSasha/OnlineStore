﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.IdentityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersInfoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("info")]
        public IActionResult GetAllUsers()
        {
            return Ok(_context.Users);
        }

    }
}
