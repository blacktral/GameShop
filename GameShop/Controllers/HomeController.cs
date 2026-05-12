

using GameShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class HomeController : Controller
{
    private readonly IGameRepository repository;

    public HomeController(IGameRepository repo)
    {
        repository = repo;
    }

    // Передаємо список усіх ігор (або акційних ігор)
    public IActionResult Home()
    {
        return View(repository.Game);
    }
}
