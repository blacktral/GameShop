using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Models
{
    [Table("cart_items")]
    public class CartItem
    {
        [Key]
        [Column("cart_item_id")]
        public int CartItemId { get; set; }

        // Це ID сесії (для гостей) або email користувача
        [Column("shopping_cart_id")]
        [StringLength(100)]
        public string ShoppingCartId { get; set; }

        [Column("amount")]
        public int Amount { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}