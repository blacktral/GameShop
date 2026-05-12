using GameShop.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameShop.Services
{
    public class TokenService
    {
        private readonly SymmetricSecurityKey _key;

        // Впроваджуємо ключ, налаштований у Program.cs
        public TokenService(SymmetricSecurityKey key)
        {
            _key = key;
        }

        public string CreateToken(User user)
        {
            // 1. Створення Claims (інформація про користувача)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role) // Роль для авторизації
            };

            // 2. Створення облікових даних підпису
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            // 3. Налаштування параметрів токена
            var token = new JwtSecurityToken(
                // issuer: null, // Можна налаштувати, якщо потрібно
                // audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(2), // Токен діє 2 години
                signingCredentials: credentials);

            // 4. Генерація та повернення токена у вигляді рядка
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}