using GameShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace GameShop.Models
{
    [Table("orders")] // Вказуємо назву таблиці в БД
    public class Order
    {
        [Key]
        [Column("order_id")] // Явно вказуємо назву стовпця order_id
        public int OrderId { get; set; } // order_id SERIAL PRIMARY KEY

        [Column("order_date")] // Явно вказуємо назву стовпця order_date
        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // order_date TIMESTAMP

        [Column("total_amount", TypeName = "decimal(10, 2)")] // Вказуємо назву стовпця та тип
        public decimal TotalAmount { get; set; } // total_amount DECIMAL(10,2)

        [Column("payment_method")] // Явно вказуємо назву стовпця payment_method
        [MaxLength(50)]
        public string PaymentMethod { get; set; } // payment_method VARCHAR(50)

        // Зовнішній ключ
        [Column("user_id")] // Явно вказуємо назву стовпця user_id
        public int UserId { get; set; } // user_id INT

        // Навігаційні властивості
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}