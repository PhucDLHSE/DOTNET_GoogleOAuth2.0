using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using SmartClass.Backend.Models;
using SmartClass.Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartClass.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _googleAudience;
        private readonly HashSet<string> _adminWhitelist;

        public AuthController(AppDbContext context)
        {
            _context = context;
            _googleAudience = Environment.GetEnvironmentVariable("GOOGLE_AUDIENCE")
                ?? throw new InvalidOperationException("GOOGLE_AUDIENCE is not set.");

            var whitelistRaw = Environment.GetEnvironmentVariable("ADMIN_WHITELIST") ?? "";
            _adminWhitelist = whitelistRaw
                .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        [HttpPost("google-mobile")]
        public async Task<IActionResult> GoogleMobileLogin([FromBody] GoogleLoginRequest request)
        {
            if (string.IsNullOrEmpty(request.IdToken))
                return BadRequest("IdToken is required");

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _googleAudience }
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = "Invalid token", details = ex.Message });
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == payload.Email && u.Provider == "google");

            if (existingUser == null)
            {
                string role = _adminWhitelist.Contains(payload.Email)
                    ? "admin"
                    : payload.Email.EndsWith("@fpt.edu.vn", StringComparison.OrdinalIgnoreCase)
                        ? "lecturer"
                        : "student";

                var newUser = new User
                {
                    Name = payload.Name,
                    Email = payload.Email,
                    Avatar = payload.Picture,
                    GoogleId = payload.Subject,
                    Provider = "google",
                    Role = role,
                    EmailVerified = true,
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                existingUser = newUser;
            }

            return Ok(new
            {
                UserId = existingUser.UserId,
                Email = existingUser.Email,
                Name = existingUser.Name,
                Avatar = existingUser.Avatar,
                Role = existingUser.Role
            });
        }
    }
}
