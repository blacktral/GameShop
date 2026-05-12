using GameShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("game_genres")] // Вказуємо назву таблиці в БД
public class GameGenre
{
    // Композитний ключ
    [Column("game_id")] // Явно вказуємо назву стовпця game_id
    public int GameId { get; set; } // game_id INT

    [Column("genres_id")] // Явно вказуємо назву стовпця genres_id
    public int GenresId { get; set; } // genres_id INT

    // Навігаційні властивості
    public Game Game { get; set; }
    public Genre Genre { get; set; }
}
