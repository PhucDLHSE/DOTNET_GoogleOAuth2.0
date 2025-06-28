using Microsoft.EntityFrameworkCore;
using SmartClass.Backend.Models;

namespace SmartClass.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<User> Users => Set<User>();
        // public DbSet<Class> Classes => Set<Class>();
        // public DbSet<ClassMember> ClassMembers => Set<ClassMember>();
        // public DbSet<Session> Sessions => Set<Session>();
        // public DbSet<Attendance> Attendances => Set<Attendance>();
        // public DbSet<Message> Messages => Set<Message>();
        // public DbSet<FaceTemplate> FaceTemplates => Set<FaceTemplate>();
    }
}
