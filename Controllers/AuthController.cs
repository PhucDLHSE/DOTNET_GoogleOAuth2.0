using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetGoogleOAuth2.Data;
using DotnetGoogleOAuth2.Models;
using DotnetGoogleOAuth2.Services;


namespace DotnetGoogleOAuth2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRoleResolver _roleResolver;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IRoleResolver roleResolver, IConfiguration config)
        {
            _context = context;
            _roleResolver = roleResolver;
            _config = config;
        }

        [HttpPost("google-mobile")]
        public async Task<IActionResult> GoogleMobileLogin([FromBody] GoogleLoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.IdToken))
                return BadRequest("IdToken is required");

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _config["GOOGLE_AUDIENCE"] ?? throw new Exception("GOOGLE_AUDIENCE not set") }
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = "Invalid token", details = ex.Message });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == payload.Email && u.Provider == "google");
            if (user == null)
            {
                user = new User
                {
                    Name = payload.Name,
                    Email = payload.Email,
                    Avatar = payload.Picture,
                    GoogleId = payload.Subject,
                    Provider = "google",
                    Role = _roleResolver.ResolveRole(payload.Email),
                    EmailVerified = true
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                userId = user.UserId,
                email = user.Email,
                name = user.Name,
                avatar = user.Avatar,
                role = user.Role
            });
        }
    }

    public class GoogleLoginRequest
    {
        public string IdToken { get; set; } = string.Empty;
    }
}
