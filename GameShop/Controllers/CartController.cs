using GameShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;

public class CartController : Controller
{
    private readonly CartService _cartService;
    private readonly ApplicationDbContext _context; // Для пошуку гри

    public CartController(CartService cartService, ApplicationDbContext context)
    {
        _cartService = cartService;
        _context = context;
    }

    public IActionResult Cart()
    {
        var items = _cartService.GetCartItems();
        ViewBag.CartTotal = _cartService.GetCartTotal();
        return View(items);
    }

    public IActionResult AddToCart(int id)
    {
        var selectedGame = _context.Game.FirstOrDefault(p => p.GameId == id);
        if (selectedGame != null)
        {
            _cartService.AddToCart(selectedGame);
        }
        return RedirectToAction("Cart");
    }

    public IActionResult RemoveFromCart(int id)
    {
        _cartService.RemoveFromCart(id);
        return RedirectToAction("Cart");
    }
}