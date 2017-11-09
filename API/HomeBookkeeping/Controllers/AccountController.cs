using HomeBookkeeping.Models;
using HomeBookkeeping.Models.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeBookkeeping.Controllers
{
    [Route("api/account")]
    [Authorize(AuthenticationSchemes = "JwtBearer")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager) => _userManager = userManager;

        public async Task<UserAPI> Get()
        {
            var email = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            return new UserAPI { Username = user?.Email };
        }
    }
}
