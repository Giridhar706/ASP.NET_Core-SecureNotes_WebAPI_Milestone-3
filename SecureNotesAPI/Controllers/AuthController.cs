using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using SecureNotesAPI.Data;
using SecureNotesAPI.Models;
using SecureNotesAPI.Services;

namespace SecureNotesAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(
            AppDbContext context,
            JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            // Check username uniqueness
            if (_context.Users.Any(
                x => x.Username == dto.Username))
            {
                return BadRequest(
                    "Username already exists");
            }

            // Basic password validation
            if (string.IsNullOrWhiteSpace(dto.Password) ||
                dto.Password.Length < 8)
            {
                return BadRequest(
                    "Password must be at least 8 characters long.");
            }

            var user = new User
            {
                Username = dto.Username,

                // Hash password before storing
                PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    dto.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message =
                "User registered successfully. Please log in."
            });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(
                x => x.Username == dto.Username);

            if (user == null)
                return Unauthorized();

            bool valid =
                BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

            if (!valid)
                return Unauthorized();

            string token =
                _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                expires_in = 3600,
                user = new
                {
                    username = user.Username
                }
            });
        }
    }
}