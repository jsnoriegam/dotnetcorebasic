using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Peliculas.Services;

namespace Peliculas.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        IAuthService AuthService;

        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }
        [HttpPost("token")]
        public IActionResult Token([FromBody] UserContext context)
        {
            if (ModelState.IsValid && AuthService.ValidateUser(context.Username, context.Password))
            {
                var now = DateTime.UtcNow;
                var validTime = TimeSpan.FromHours(2);
                var expires = now.Add(validTime);
                var token = AuthService.GenerateAccessToken(now, context.Username, validTime);
                return Ok(new {
                    Token = token,
                    ExpiresAt = expires
                });
            }
            else
            {
                return StatusCode(401);
            }
        }
    }

    public class UserContext
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}