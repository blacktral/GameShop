using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Models
{
    [Table("game")]
    public class Game
    {
        [Key]
        [Column("game_id")]
        public int GameId { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(200)]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("price", TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column("release_date")]
        public DateOnly ReleaseDate { get; set; }

        [Column("developer")]
        [MaxLength(150)]
        public string Developer { get; set; }

        [Column("publisher")]
        [MaxLength(150)]
        public string Publisher { get; set; }  // 🆕 нове поле

        [Column("cover_image_url")]
        [MaxLength(255)]
        public string CoverImageUrl { get; set; }

        [Column("trailer_url")]
        [MaxLength(255)]
        public string? TrailerUrl { get; set; }  // 🆕 нове поле

        [Column("rating")]
        public decimal? Rating { get; set; }  // 🆕 середній рейтинг

        [Column("is_active")]
        public bool IsActive { get; set; } = true;  // 🆕 активність гри

        [Column("external_id")]
        [MaxLength(100)]
        public string? ExternalId { get; set; }  // 🆕 ID з RAWG API

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 🆕 час створення

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Навігаційні властивості
        public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
        public ICollection<UserLibrary> UserLibrary { get; set; } = new List<UserLibrary>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

