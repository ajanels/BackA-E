using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendAE.Data;
using BackendAE.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public class LoginRequest
        {
            public string UsuId { get; set; }
            public string UsuContraseña { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest userRequest)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuId == userRequest.UsuId);

            if (usuario == null || usuario.UsuContraseña != userRequest.UsuContraseña)
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            string token = GenerateJwtToken(usuario);
            return Ok(new { token });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UsuId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("rol", usuario.RolId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
