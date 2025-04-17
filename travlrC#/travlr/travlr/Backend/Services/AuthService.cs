

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using travlr.Backend.Repositories;
using travlr.Backend.Models;




namespace travlr.Backend.Service
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> Login(User user)
        {
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser == null || existingUser.Password != user.Password) // Use hashing here in real app
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return GenerateJwtToken(existingUser);
        }

        public async Task<string> Register(User user)
        {
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            await _userRepository.CreateUser(user);
            return GenerateJwtToken(user);
        }

        public bool IsLoggedIn(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                return jsonToken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }

        public User GetCurrentUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var email = jsonToken?.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var name = jsonToken?.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

            return new User { Email = email, Name = name };
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
