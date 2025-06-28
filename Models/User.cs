using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetGoogleOAuth2.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string? Avatar { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [MaxLength(20)]
        public string? Phone { get; set; }

        public string? Password { get; set; }

        public string? GoogleId { get; set; }

        [Required]
        public string Role { get; set; } = "student";

        [Required]
        public string Provider { get; set; } = "local";

        public bool EmailVerified { get; set; } = false;

        public bool IsLocked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}