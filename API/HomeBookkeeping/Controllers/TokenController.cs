using HomeBookkeeping.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace HomeBookkeeping.Controllers
{
    public class LogoutModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<AuthOptions> _options;

        public TokenController(UserManager<User> userManager, IOptions<AuthOptions> options)
        {
            _userManager = userManager;
            _options = options;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]LogoutModel model)
        {
            var user = _userManager.FindByNameAsync(model.Username).Result;

            if (user == null || !_userManager.CheckPasswordAsync(user, model.Password).Result) return StatusCode(400);

            var token = GenerationToken(user);

            return Ok(new { token });
        }

        private string GenerationToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = _options.Value.GetSymmetricSecurityKey();
            var creds = new SigningCredentials(_options.Value.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256);

            var date = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                issuer: _options.Value.ISSUER,
                audience: _options.Value.AUDIENCE,
                notBefore: date,
                claims: claims,
                expires: date.Add(TimeSpan.FromMinutes(_options.Value.LIFETIME)),
                signingCredentials: new SigningCredentials(_options.Value.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
