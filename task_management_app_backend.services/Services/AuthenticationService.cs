using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;
using task_management_app_backend.resources.Entities;
using task_management_app_backend.services.IServices;

namespace task_management_app_backend.services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        public bool Register(CreateUserDto newUser)
        {
            // Check if user already exists
            var existingUser = _userRepository.GetUser(newUser.email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            // Hash the password before saving
            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = newUser.userName,
                Email = newUser.email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.passWord),
                Role = newUser.role
            };

            return _userRepository.Add(user);
        }

        public LoginResponseDto Login(LoginRequestDto loginRequest)
        {
            var user = _userRepository.GetUser(loginRequest.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                email = user.Email,
                userName = user.UserName,
                role = user.Role
            };
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
