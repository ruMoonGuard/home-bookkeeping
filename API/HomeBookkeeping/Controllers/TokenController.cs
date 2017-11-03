using HomeBookkeeping.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeBookkeeping.Controllers
{
    public class ModelX
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly UserManager<User> _userManager;

        public TokenController(UserManager<User> userManager) => _userManager = userManager;

        [HttpPost, Route("create")]
        public async Task Create([FromBody]ModelX model)
        {
            var identity = GetIdentity(model.Username, model.Password).Result;

            if (identity == null)
            {
                // TODO : error
            }

            var nowTime = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: nowTime,
                claims: identity.Claims,
                expires: nowTime.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new
            {
                token = encodedJwt,
                name = identity.Name,
                expires = nowTime.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME))
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response,
                new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            var users = _userManager.Users.ToList();

            if (user == null || !await _userManager.CheckPasswordAsync(user, password)) return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Nbf,
                    new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,
                    new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
