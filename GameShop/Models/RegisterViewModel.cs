using System.ComponentModel.DataAnnotations;

namespace GameShop.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введіть ім'я користувача.")]
        [Display(Name = "Ім'я користувача")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Ім'я має бути від 3 до 100 символів.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введіть коректний Email.")]
        [EmailAddress(ErrorMessage = "Некоректний формат Email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль має бути не менше 6 символів.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Підтвердіть пароль.")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }
    }
}
