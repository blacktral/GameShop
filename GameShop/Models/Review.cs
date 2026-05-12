using GameShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("reviews")] // Вказуємо назву таблиці в БД
public class Review
{
    [Key]
    [Column("review_id")] // Явно вказуємо назву стовпця review_id
    public int ReviewId { get; set; } // review_id SERIAL PRIMARY KEY

    [Range(1, 10)]
    [Column("rating")] // Тут також краще явно вказати назву стовпця
    public int Rating { get; set; } // rating INT CHECK (rating BETWEEN 1 AND 10)

    [Column("comment")]
    public string Comment { get; set; } // comment TEXT

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP

    // Зовнішні ключі
    [Column("game_id")] // Явно вказуємо назву стовпця game_id
    public int GameId { get; set; } // game_id INT

    [Column("user_id")] // Явно вказуємо назву стовпця user_id
    public int UserId { get; set; } // user_id INT

    // Навігаційні властивості
    public Game Game { get; set; }
    public User User { get; set; }
}
