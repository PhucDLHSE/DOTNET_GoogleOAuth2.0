using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using DotnetGoogleOAuth2.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using DotnetGoogleOAuth2.Data;


var builder = WebApplication.CreateBuilder(args);

// ✅ Load cấu hình từ .env
Env.Load();

string googleClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")
    ?? throw new InvalidOperationException("GOOGLE_CLIENT_ID is not set.");
string googleClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET")
    ?? throw new InvalidOperationException("GOOGLE_CLIENT_SECRET is not set.");
string googleCallbackPath = Environment.GetEnvironmentVariable("GOOGLE_CALLBACK_URL") ?? "/auth/google/callback";
string googleAudience = Environment.GetEnvironmentVariable("GOOGLE_AUDIENCE")
    ?? throw new InvalidOperationException("GOOGLE_AUDIENCE is not set.");
string[] adminWhitelist = Environment.GetEnvironmentVariable("ADMIN_WHITELIST")?.Split(',') ?? Array.Empty<string>();
string connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING")
    ?? throw new InvalidOperationException("MYSQL_CONNECTION_STRING is not set.");

// ✅ Đăng ký dịch vụ
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();

// ✅ Inject custom role resolver và biến cấu hình
builder.Services.AddSingleton<IRoleResolver, DefaultRoleResolver>();
builder.Services.AddSingleton(adminWhitelist);
builder.Services.AddSingleton(googleAudience);

var app = builder.Build();

// ✅ Middleware pipeline
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
