using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration; //appsettingjson okuyabilmek için
    private readonly SirketData.SirketDbContext _context;

    public AuthController(IConfiguration configuration, SirketData.SirketDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // 1. Koddan admin kontrolü
        if (model.Username == "admin" && model.Password == "admiN123450")
        {
            var token = GenerateJwtToken("admin", "Admin");
            return Ok(new { token });
        }

        // 2. Diğer kullanıcılar için veritabanı kontrolü
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (user == null)
            return Unauthorized();

        var inputPasswordHash = ComputeSha256Hash(model.Password);
        if (user.PasswordHash != inputPasswordHash)
            return Unauthorized();

        var token2 = GenerateJwtToken(user.Username, user.Role);
        return Ok(new { token = token2 });
    }

    private string ComputeSha256Hash(string rawData)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }

    private string GenerateJwtToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
