using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GameShop.Models
{

    [Table("genres")] // Вказуємо назву таблиці в БД
    public class Genre
    {
        [Key]
        [Column("genres_id")] // Явно вказуємо назву стовпця genres_id
        public int GenresId { get; set; } // genres_id SERIAL PRIMARY KEY

        [Required]
        [Column("name_genres")] // Явно вказуємо назву стовпця name_genres
        [MaxLength(100)]
        public string NameGenres { get; set; } // name_genres VARCHAR(100)

        [Column("description_genres")] // Явно вказуємо назву стовпця description_genres
        public string DescriptionGenres { get; set; } // description_genres TEXT

        // Навігаційна властивість
        public ICollection<GameGenre> GameGenres { get; set; }
    }
}
