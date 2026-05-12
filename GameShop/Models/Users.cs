using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Models
{
    [Table("users")] // Вказуємо назву таблиці в БД
    public class User
    {
        [Key]
        [Column("user_id")] // Явно вказуємо назву стовпця в БД
        public int UserId { get; set; } // Стиль C# (PascalCase)

        [Required]
        [Column("username")]
        [MaxLength(100)]
        public string Username { get; set; } // Стиль C# (PascalCase)

        [Required]
        [Column("email")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [Column("password_hash")]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Column("role")]
        public string Role { get; set; } = "user"; // role VARCHAR(50) DEFAULT 'user'
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

        // --- ВАШЕ НОВЕ ПОЛЕ ДЛЯ АВАТАРКИ ---
        [Column("avatar_url")]
        [MaxLength(500)] // URL або шлях до файлу
        public string? AvatarUrl { get; set; } // Може бути null
        // ------------------------------------

        // Навігаційні властивості (зв'язки)
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserLibrary> UserLibrary { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}


