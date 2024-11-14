using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DuAn_DTNC.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DuAn_DTNC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DTNCDbContext _dbContext;

        public TokenController(IConfiguration configuration, DTNCDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User _user)
        {
            if (_user != null && !string.IsNullOrEmpty(_user.Username) && !string.IsNullOrEmpty(_user.Password))
            {
                var user = await GetUser(_user.Username, _user.Password);
                if (user != null)
                {
                    var token = GenerateJwtToken(user);
                    return Ok(new { token });
                }
                else
                {
                    return Unauthorized();
                }
            }
            return BadRequest("Invalid client request");
        }

        private async Task<User> GetUser(string username, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FullName ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role ?? "user"), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
