using GameShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("order_items")] // Вказуємо назву таблиці в БД
public class OrderItem
{
    [Key]
    [Column("order_item_id")] // Явно вказуємо назву стовпця order_item_id
    public int OrderItemId { get; set; } // order_item_id SERIAL PRIMARY KEY

    [Column("unit_price", TypeName = "decimal(10, 2)")] // Назва стовпця та тип
    public decimal UnitPrice { get; set; } // unit_price DECIMAL(10,2)

    // Зовнішні ключі
    [Column("order_id")] // Явно вказуємо назву стовпця order_id
    public int OrderId { get; set; } // order_id INT

    [Column("game_id")] // Явно вказуємо назву стовпця game_id
    public int GameId { get; set; } // game_id INT

    // Навігаційні властивості
    public Order Order { get; set; }
    public Game Game { get; set; }
}
