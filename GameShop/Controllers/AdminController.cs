using GameShop.Models;
using GameShop.Services;
using GameShop.Services.Rawg.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRawgApiService _rawgService;
        // --- ВИДАЛЕНО UserManager та RoleManager ---

        // --- ОНОВЛЕНО КОНСТРУКТОР ---
        public AdminController(ApplicationDbContext context,
                               IRawgApiService rawgService)
        {
            _context = context;
            _rawgService = rawgService;
        }

        public IActionResult Admin()
        {
            return View(); // Головна сторінка адмінки
        }

        // =================================================================
        //                      ФУНКЦІОНАЛ ІМПОРТУ (AddGame)
        // =================================================================

        [HttpGet]
        public IActionResult AddGame()
        {
            return View(new List<RawgGameSummary>());
        }

        [HttpPost]
        public async Task<IActionResult> SearchRawg(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewData["Error"] = "Будь ласка, введіть назву гри для пошуку.";
                return View("AddGame", new List<RawgGameSummary>());
            }
            try
            {
                var response = await _rawgService.SearchGamesAsync(query);
                if (response?.Results != null)
                {
                    var results = response.Results.Where(r => !_context.Game.Any(g => g.ExternalId == r.Id.ToString())).ToList();
                    if (!results.Any()) ViewData["Message"] = $"Ігри за запитом '{query}' не знайдено, або всі знайдені ігри вже імпортовані.";
                    return View("AddGame", results);
                }
                ViewData["Message"] = $"Ігри за запитом '{query}' не знайдено.";
                return View("AddGame", new List<RawgGameSummary>());
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Помилка пошуку: {ex.Message}";
                return View("AddGame", new List<RawgGameSummary>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportGameDetails(int rawgId)
        {
            if (rawgId <= 0)
            {
                TempData["Error"] = "Недійсний RAWG ID.";
                return RedirectToAction(nameof(AddGame));
            }
            if (_context.Game.Any(g => g.ExternalId == rawgId.ToString()))
            {
                TempData["Error"] = $"Гра з RAWG ID {rawgId} вже присутня в базі даних.";
                return RedirectToAction(nameof(AddGame));
            }
            try
            {
                RawgGameDetails rawgGame = await _rawgService.GetGameDetailsAsync(rawgId);
                if (rawgGame == null)
                {
                    TempData["Error"] = $"Гра з ID {rawgId} не знайдена в RAWG API.";
                    return RedirectToAction(nameof(AddGame));
                }
                var game = new Game
                {
                    ExternalId = rawgGame.Id.ToString(),
                    Title = rawgGame.Title,
                    Description = rawgGame.Description,
                    CoverImageUrl = rawgGame.CoverImageUrl,
                    ReleaseDate = rawgGame.Released.GetValueOrDefault(),
                    Rating = rawgGame.Rating,
                    Developer = rawgGame.Developers?.FirstOrDefault()?.Name ?? "Невідомий",
                    Publisher = rawgGame.Publishers?.FirstOrDefault()?.Name ?? "Невідомий",
                    TrailerUrl = rawgGame.Clip?.TrailerUrl,
                    Price = 0.00m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Game.Add(game);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Гра '{game.Title}' успішно імпортована! Встановіть ціну.";
                return RedirectToAction(nameof(Admin));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Помилка імпорту: {ex.Message}";
                return RedirectToAction(nameof(AddGame));
            }
        }


        // =================================================================
        //            ФУНКЦІОНАЛ РЕДАГУВАННЯ ТА СПИСКУ ІГОР
        // =================================================================

        [HttpGet]
        public async Task<IActionResult> GameList()
        {
            var games = await _context.Game.AsNoTracking().OrderBy(g => g.Title).ToListAsync();
            return View(games);
        }

        [HttpGet]
        public async Task<IActionResult> EditGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGame(int id, Game gameFromForm)
        {
            if (id != gameFromForm.GameId)
            {
                return BadRequest();
            }

            var gameToUpdate = await _context.Game.FindAsync(id);
            if (gameToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    gameToUpdate.Title = gameFromForm.Title;
                    gameToUpdate.Description = gameFromForm.Description;
                    gameToUpdate.Price = gameFromForm.Price;
                    gameToUpdate.ReleaseDate = gameFromForm.ReleaseDate;
                    gameToUpdate.Developer = gameFromForm.Developer;
                    gameToUpdate.Publisher = gameFromForm.Publisher;
                    gameToUpdate.CoverImageUrl = gameFromForm.CoverImageUrl;
                    gameToUpdate.TrailerUrl = gameFromForm.TrailerUrl;
                    gameToUpdate.IsActive = gameFromForm.IsActive;
                    gameToUpdate.UpdatedAt = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"Дані гри '{gameToUpdate.Title}' успішно оновлено!";
                    return RedirectToAction(nameof(GameList));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Game.Any(e => e.GameId == gameToUpdate.GameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(gameFromForm);
        }

        // --- ДОДАНО: ЛОГІКА ВИДАЛЕННЯ ГРИ ---
        // (Для кнопки "Видалити" у GameList.cshtml)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                TempData["Error"] = "Помилка: Гру не знайдено.";
                return RedirectToAction(nameof(GameList));
            }
            try
            {
                _context.Game.Remove(game);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Гру '{game.Title}' було успішно видалено.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Помилка видалення гри. {ex.Message}";
            }
            return RedirectToAction(nameof(GameList));
        }


        // =================================================================
        //            ФУНКЦІОНАЛ КЕРУВАННЯ РОЛЯМИ
        // =================================================================

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            var userRolesViewModelList = users.Select(user => new UserRolesViewModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Roles = new List<string> { user.Role }
            }).ToList();

            return View(userRolesViewModelList.OrderBy(u => u.Username).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRole(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                TempData["Error"] = "Користувача не знайдено.";
                return RedirectToAction(nameof(UserList));
            }

            var allRoles = await _context.Users
                                     .Select(u => u.Role)
                                     .Distinct()
                                     .ToListAsync();

            var model = new EditUserRoleViewModel
            {
                UserId = user.UserId,
                Username = user.Username, // Використовуємо 'Username' (з малої 'n')
                SelectedRole = user.Role,
                RolesList = allRoles.Select(role => new SelectListItem
                {
                    Text = role,
                    Value = role,
                    Selected = role == user.Role
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserRole(EditUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var allRoles = await _context.Users.Select(u => u.Role).Distinct().ToListAsync();
                model.RolesList = allRoles.Select(role => new SelectListItem
                {
                    Text = role,
                    Value = role,
                    Selected = role == model.SelectedRole
                });
                return View(model);
            }

            var user = await _context.Users.FindAsync(model.UserId);
            if (user == null)
            {
                TempData["Error"] = "Користувача не знайдено.";
                return RedirectToAction(nameof(UserList));
            }

            try
            {
                user.Role = model.SelectedRole;
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Роль для користувача '{user.Username}' успішно оновлено.";
                return RedirectToAction(nameof(UserList));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Помилка при оновленні ролі: {ex.Message}";
                var allRoles = await _context.Users.Select(u => u.Role).Distinct().ToListAsync();
                model.RolesList = allRoles.Select(role => new SelectListItem
                {
                    Text = role,
                    Value = role,
                    Selected = role == model.SelectedRole
                });
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                TempData["Error"] = "Помилка: Користувача не знайдено.";
                return RedirectToAction(nameof(UserList));
            }

            // === ОНОВЛЕНА ПЕРЕВІРКА: Запобігання видаленню адмінів ===
            // Тепер ми перевіряємо роль користувача, якого намагаємось видалити.
            // Припускаємо, що роль адміна називається "admin".
            if (user.Role.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Помилка: Не можна видалити користувача з роллю 'admin'.";
                return RedirectToAction(nameof(UserList));
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Користувача '{user.Username}' було успішно видалено.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Error"] = $"Помилка видалення. Можливо, цей користувач має пов'язані дані (наприклад, замовлення). {ex.InnerException?.Message}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Сталася непередбачувана помилка: {ex.Message}";
            }

            return RedirectToAction(nameof(UserList));
        }
    }
}