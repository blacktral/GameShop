using GameShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShop.Controllers
{
    [Authorize] // Тільки для зареєстрованих
    public class UserLibraryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserLibraryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult UserLibrary()
        {
            // Отримуємо ID поточного користувача
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "UserAccount");

            int userId = int.Parse(userIdStr);

            // Витягуємо список ігор, якими володіє цей юзер
            // Include(ul => ul.Game) дуже важливий, щоб підтягнути назву і картинку!
            var myGames = _context.UserLibraries
                .Where(ul => ul.UserId == userId)
                .Include(ul => ul.Game)
                .Select(ul => ul.Game) // Нам потрібні самі об'єкти ігор
                .ToList();

            return View(myGames);
        }
    }
}