using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegistrationDTO registrationDTO)
        {
            if (_context.Users.Any(u => u.Username == registrationDTO.Username))
                throw new Exception("Username already exists.");

            var user = new User
            {
                Username = registrationDTO.Username,
                Email = registrationDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registrationDTO.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDTO
            {
                Username = user.Username,
                Role = user.Role,
                Token = GenerateJwtToken(user)
            };
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginDTO.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
                throw new Exception("Invalid username or password.");

            return new AuthResponseDTO
            {
                Username = user.Username,
                Role = user.Role,
                Token = GenerateJwtToken(user)
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
