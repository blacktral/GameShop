using GameShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


// Клас для з'єднання "Багато-до-Багатьох" (User <-> Game)
[Table("user_library")] // Вказуємо назву таблиці в БД
public class UserLibrary
{
    // Композитний ключ (навігаційні властивості не потребують атрибутів Column,
    // оскільки вони визначаються через HasKey у DbContext)
    [Column("user_id")]
    public int UserId { get; set; } // user_id INT

    [Column("game_id")]
    public int GameId { get; set; } // game_id INT

    [Column("acquired_time")]
    public DateTime AcquiredTime { get; set; } = DateTime.UtcNow; // acquired_time TIMESTAMP

    // Навігаційні властивості
    public User User { get; set; }
    public Game Game { get; set; }
}
