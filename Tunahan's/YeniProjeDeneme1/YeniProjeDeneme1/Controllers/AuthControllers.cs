using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Dtos;
using YeniProjeDeneme1.Services;
namespace YeniProjeDeneme1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        public readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public IActionResult Login(Login login)
        {
            bool bakalim = _authService.IsAdmin(login);
            if (bakalim)
            {
                var token = GenerateJwtToken("Admin");
                return Ok(new { token });
            }
            else           {
                var token = GenerateJwtToken( "User");
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bunu-sakinnn-32-karakter-uzat-****"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
