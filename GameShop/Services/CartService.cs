using GameShop.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class CartService
{
    private readonly ApplicationDbContext _context;
    private readonly string _shoppingCartId;

    public CartService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        // Отримуємо ID сесії. Якщо його немає — створюємо новий.
        ISession session = httpContextAccessor.HttpContext.Session;
        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        session.SetString("CartId", cartId);

        _shoppingCartId = cartId;
    }

    public void AddToCart(Game game)
    {
        var cartItem = _context.CartItems.SingleOrDefault(
            s => s.GameId == game.GameId && s.ShoppingCartId == _shoppingCartId);

        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                ShoppingCartId = _shoppingCartId,
                Game = game,
                Amount = 1,
                CreatedAt = DateTime.UtcNow
            };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Amount++;
        }
        _context.SaveChanges();
    }

    public void RemoveFromCart(int gameId)
    {
        var cartItem = _context.CartItems.SingleOrDefault(
            s => s.GameId == gameId && s.ShoppingCartId == _shoppingCartId);

        if (cartItem != null)
        {
            if (cartItem.Amount > 1)
            {
                cartItem.Amount--;
            }
            else
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }
    }

    public List<CartItem> GetCartItems()
    {
        return _context.CartItems
            .Where(c => c.ShoppingCartId == _shoppingCartId)
            .Include(s => s.Game)
            .ToList();
    }

    public decimal GetCartTotal()
    {
        return _context.CartItems
            .Where(c => c.ShoppingCartId == _shoppingCartId)
            .Select(c => c.Game.Price * c.Amount)
            .Sum();
    }

    public void ClearCart()
    {
        var cartItems = _context.CartItems
            .Where(c => c.ShoppingCartId == _shoppingCartId);

        _context.CartItems.RemoveRange(cartItems);
        _context.SaveChanges();
    }
}