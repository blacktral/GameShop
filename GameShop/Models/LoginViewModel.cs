using System.ComponentModel.DataAnnotations;

namespace GameShop.Models
{
    // Ця модель збирає дані з форми
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле логіну є обов'язковим.")]
        [Display(Name = "Логін (Email або телефон)")]
        // Використовуємо string для прийому як email, так і телефону
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле паролю є обов'язковим.")]
        [DataType(DataType.Password)] // Приховує введений текст
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
