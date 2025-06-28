using Microsoft.EntityFrameworkCore;
using DotnetGoogleOAuth2.Models;

namespace DotnetGoogleOAuth2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}