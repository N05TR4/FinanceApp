using FinanceApp.Infraestructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using FinanceApp.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceApp.Infraestructure.Context;
using AutoMapper;
using FinanceApp.Domain.Entities;

namespace FinanceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly FinanceAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthController(IOptions<JwtSettings> jwtSettings, FinanceAppDbContext dbContext, IMapper mapper)
        {
            _jwtSettings = jwtSettings.Value;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel loginModel)
        {
            try
            {
                var user = _dbContext.Usuario
                    .FirstOrDefault(u => u.Email == loginModel.Email && u.Contraseña == loginModel.Contraseña);

                if (user != null)
                {
                    var userModel = _mapper.Map<UsuarioModels>(user);
                    var token = GenerateJwtToken(userModel);
                    return Ok(new { token });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }

        private string GenerateJwtToken(UsuarioModels user)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UsuarioID.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.Nombre} {user.Apellido}")
                };

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireTimeInMinutes),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating JWT token", ex);
            }
        }
    }
}