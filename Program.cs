using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetEnv;
using SmartClass.Backend.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ---------- Load biến môi trường ----------
Env.Load();

// ---------- Cấu hình từ biến môi trường ----------
var googleClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")
    ?? throw new InvalidOperationException("GOOGLE_CLIENT_ID is not set.");

var googleClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET")
    ?? throw new InvalidOperationException("GOOGLE_CLIENT_SECRET is not set.");

var rawCallbackUrl = Environment.GetEnvironmentVariable("GOOGLE_CALLBACK_URL");
var googleCallbackPath = string.IsNullOrWhiteSpace(rawCallbackUrl)
    ? "/auth/google/callback"
    : rawCallbackUrl.StartsWith("/") ? rawCallbackUrl : "/" + rawCallbackUrl;

var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING")
    ?? throw new InvalidOperationException("MYSQL_CONNECTION_STRING is not set.");

// ---------- Đăng ký Service ----------

// ✅ Google OAuth + Cookie
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = googleClientId;
    options.ClientSecret = googleClientSecret;
    options.CallbackPath = googleCallbackPath;
    options.SaveTokens = true;
});

// ✅ CORS (cho mobile app gọi thoải mái)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ✅ EF Core + MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// ✅ Controller API
builder.Services.AddControllers();

// ---------- Build App ----------
var app = builder.Build();

app.UseRouting();
app.UseCors();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.Run();
