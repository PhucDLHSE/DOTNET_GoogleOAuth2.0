using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClass.Backend.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; } = "";

        [MaxLength(20)]
        [Column("phone")]
        public string? Phone { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("google_id")]
        public string? GoogleId { get; set; }

        [Required]
        [Column("role")]
        public string Role { get; set; } = "student"; // admin | lecturer | student

        [Required]
        [Column("provider")]
        public string Provider { get; set; } = "local"; // local | google

        [Column("email_verified")]
        public bool EmailVerified { get; set; } = false;

        [Column("is_locked")]
        public bool IsLocked { get; set; } = false;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
