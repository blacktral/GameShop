using GameShop.Models;
using GameShop.Services; // Перевір, чи CartService тут
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore; // Потрібно для Include, якщо знадобиться

namespace GameShop.Controllers
{
    [Authorize] // Лише авторизовані користувачі можуть купувати
    public class OrderController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;

        public OrderController(CartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        // 1. GET: Показати форму оформлення
        [HttpGet]
        public IActionResult Checkout()
        {
            // Перевіряємо, чи не порожній кошик, перш ніж показувати сторінку
            var items = _cartService.GetCartItems();
            if (items.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            // Передаємо суму в View, щоб показати її користувачу
            ViewBag.TotalAmount = _cartService.GetCartTotal().ToString("C2");

            return View();
        }

        // 2. POST: Обробити покупку
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _cartService.GetCartItems();

            // Перевірка 1: Чи не порожній кошик
            if (items.Count == 0)
            {
                ModelState.AddModelError("", "Ваш кошик порожній");
                return RedirectToAction("Index", "Cart");
            }

            // Перевірка 2: (FIX) Якщо з якоїсь причини метод оплати null — ставимо заглушку
            if (string.IsNullOrEmpty(order.PaymentMethod))
            {
                order.PaymentMethod = "Card";
            }

            // Отримуємо ID користувача
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Якщо раптом ID немає (хоча [Authorize] це блокує), перестрахуємось
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "UserAccount");
            }

            int userId = int.Parse(userIdString);

            // --- ЕТАП 1: Створення Замовлення ---
            order.UserId = userId;
            order.OrderDate = DateTime.UtcNow;
            order.TotalAmount = _cartService.GetCartTotal();

            _context.Orders.Add(order);
            _context.SaveChanges(); // Генеруємо OrderId

            // --- ЕТАП 2: Обробка кожного товару ---
            foreach (var item in items)
            {
                // А. Додаємо в історію покупок (OrderItems)
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    GameId = item.Game.GameId,
                    UnitPrice = item.Game.Price // Фіксуємо ціну!
                };
                _context.OrderItems.Add(orderItem);

                // Б. Додаємо в БІБЛІОТЕКУ користувача (UserLibraries)
                // Перевіряємо, чи немає вже такої гри, щоб не було дублікатів gfdgdfhgdfgdf авыавыавыавыайывпывп
                bool alreadyOwned = _context.UserLibraries.Any(ul => ul.UserId == userId && ul.GameId == item.Game.GameId);

                if (!alreadyOwned)
                {
                    var userLib = new UserLibrary
                    {
                        UserId = userId,
                        GameId = item.Game.GameId,
                    };
                    _context.UserLibraries.Add(userLib);
                }
            }

            // Зберігаємо всі зміни (OrderItems та UserLibraries)gfgf ffffffff
            _context.SaveChanges();

            // --- ЕТАП 3: Фінал ---
            _cartService.ClearCart(); // Очищаємо кошик

            return RedirectToAction("CheckoutComplete");
        }

        // 3. Сторінка успіху
        public IActionResult CheckoutComplete()
        {
            ViewBag.Message = "Дякуємо за покупку! Ігри додано до вашої бібліотеки.";
            return View();
        }
    }
}