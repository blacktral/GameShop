using GameShop.Models;
using Microsoft.AspNetCore.Mvc;

public class GameInfoController : Controller
{
    private readonly IGameRepository repository;

    public GameInfoController(IGameRepository repo)
    {
        repository = repo;
    }

    // Дія GameInfo приймає ідентифікатор гри
    public IActionResult GameInfo(int id)
    {
        // 1. ЗНАЙТИ ГРУ: Використовуємо ID для пошуку гри в репозиторії
        Game game = repository.Game.FirstOrDefault(g => g.GameId == id);

        if (game == null)
        {
            // Якщо гра не знайдена, можна перенаправити на 404 або список
            return NotFound();
        }

        // 2. ПЕРЕДАТИ ГРУ: Передаємо об'єкт однієї гри у представлення (View)
        return View(game);
    }
}
