using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GameShop.Models
{
    public class SeedData
    {
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            // Отримуємо контекст бази даних
            ApplicationDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Встановлюємо дату та час створення для початкових даних
            var now = DateTime.UtcNow;

            if (!context.Game.Any())
            {
                context.Game.AddRange(

                // 1. Hollow Knight: Silksong
                new Game
                {
                    Title = "Hollow Knight: Silksong",
                    ReleaseDate = new DateOnly(2025, 01, 01),
                    CoverImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR8mPl-xVeLXGdV5jCZEQBjnlpHxXUtzBL6-ypExo95M71r2z0vRpyvAu5Z2MGKplzbSn8gP0bgaJIy1w4Es21taAmFXz3b-3_vd98816nWCA",
                    Description = "Дослідіть величезне прокляте царство у Hollow Knight: Silksong! Відкривайте його таємниці, боріться та боріться за своє життя, піднімаючись до вершин земель, де правлять шовк та пісня.",
                    Developer = "Team Cherry",
                    Price = 415,
                    Publisher = "Team Cherry", // Додано
                    TrailerUrl = "https://www.youtube.com/embed/pGg5D-m9eBw", // Додано
                    Rating = 0.0m, // Додано
                    IsActive = true, // Додано
                    ExternalId = null, // Додано
                    CreatedAt = now, // Додано
                    UpdatedAt = null // Додано
                },

                // 2. S.T.A.L.K.E.R. 2: Heart of Chornobyl
                new Game
                {
                    Title = "S.T.A.L.K.E.R. 2: Heart of Chornobyl",
                    ReleaseDate = new DateOnly(2024, 01, 01),
                    CoverImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR8mPl-xVeLXGdV5jCZEQBjnlpHxXUtzBL6-ypExo95M71r2z0vRpyvAu5Z2MGKplzbSn8gP0bgaJIy1w4Es21taAmFXz3b-3_vd98816nWCA",
                    Description = "Досліджуйте Чорнобильську Зону Відчуження повну небезпечних ворогів, смертельних аномалій та потужних артефактів. Напишіть свою епічну історію, прокладаючи стежки до Серця Чорнобиля. Вибирайте свій шлях обдумано, адже він визначить вашу долю наприкінці.",
                    Developer = "GSC Game World",
                    Price = 1399,
                    Publisher = "GSC Game World", // Додано
                    TrailerUrl = "https://www.youtube.com/embed/5D3xKxT3XwM", // Додано
                    Rating = 0.0m, // Додано
                    IsActive = true, // Додано
                    ExternalId = null, // Додано
                    CreatedAt = now, // Додано
                    UpdatedAt = null // Додано
                },

                // 3. Kingdom Come: Delieverence 2
                new Game
                {
                    Title = "Kingdom Come: Delieverence 2",
                    ReleaseDate = new DateOnly(2025, 01, 01),
                    CoverImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR8mPl-xVeLXGdV5jCZEQBjnlpHxXUtzBL6-ypExo95M71r2z0vRpyvAu5Z2MGKplzbSn8gP0bgaJIy1w4Es21taAmFXz3b-3_vd98816nWCA",
                    Description = "Захоплюючий сюжетний рольовий екшен із багатим відкритим світом, дія якого розгортається в Європі XV століття. Погляньте на середньовічне життя очима молодого Індржиха, вирушивши у подорож епічного масштабу.",
                    Developer = "Warhorse Studio",
                    Price = 1599,
                    Publisher = "Deep Silver", // Додано
                    TrailerUrl = "https://www.youtube.com/embed/3Wf2S4E480w", // Додано
                    Rating = 0.0m, // Додано
                    IsActive = true, // Додано
                    ExternalId = null, // Додано
                    CreatedAt = now, // Додано
                    UpdatedAt = null // Додано
                },

                // 4. Cyberpunk 2077
                new Game
                {
                    Title = "Cyberpunk 2077",
                    ReleaseDate = new DateOnly(2020, 01, 01),
                    CoverImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR8mPl-xVeLXGdV5jCZEQBjnlpHxXUtzBL6-ypExo95M71r2z0vRpyvAu5Z2MGKplzbSn8gP0bgaJIy1w4Es21taAmFXz3b-3_vd98816nWCA",
                    Description = "Cyberpunk 2077 - пригодницька рольова гра з відкритим світом, дія якої відбувається у футуристичному мегаполісі Найт-Сіті, де найбільше цінуються влада, розкіш та модифікації тіла.",
                    Developer = "CD Projekt Red",
                    Price = 1399,
                    Publisher = "CD Projekt", // Додано
                    TrailerUrl = "https://www.youtube.com/embed/LembR9q5_8w", // Додано
                    Rating = 8.5m, // Додано
                    IsActive = true, // Додано
                    ExternalId = "326245", // Додано
                    CreatedAt = now, // Додано
                    UpdatedAt = null // Додано
                },

                // 5. The Witcher 3: Wild Hunt
                new Game
                {
                    Title = "The Witcher 3: Wild Hunt",
                    ReleaseDate = new DateOnly(2015, 01, 01),
                    CoverImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR8mPl-xVeLXGdV5jCZEQBjnlpHxXUtzBL6-ypExo95M71r2z0vRpyvAu5Z2MGKplzbSn8gP0bgaJIy1w4Es21taAmFXz3b-3_vd98816nWCA",
                    Description = "Ви — Геральт із Рівії, найманець убивця чудовиськ. Ви подорожуєте світом, в якому вирує війна і на кожному кроці підстерігають чудовиська. Ви маєте виконати замовлення і знайти Цирі — Дитя Призначення, живу зброю, здатну змінити вигляд цього світу",
                    Developer = "CD Projekt Red",
                    Price = 949,
                    Publisher = "CD Projekt", // Додано
                    TrailerUrl = "https://www.youtube.com/embed/c0i88t0Kacs", // Додано
                    Rating = 9.8m, // Додано
                    IsActive = true, // Додано
                    ExternalId = "32", // Додано
                    CreatedAt = now, // Додано
                    UpdatedAt = null // Додано
                },

                // 6. Forza Horizon 5
                new Game
                {
                    Title = "Forza Horizon 5",
                    ReleaseDate = new DateOnly(2021, 01, 01),
                    CoverImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR8mPl-xVeLXGdV5jCZEQBjnlpHxXUtzBL6-ypExo95M71r2z0vRpyvAu5Z2MGKplzbSn8gP0bgaJIy1w4Es21taAmFXz3b-3_vd98816nWCA",
                    Description = "Дослідіть яскраві пейзажі Мексики у відкритому світі з безмежним, захоплюючим рухом на найкращих у світі автомобілях.",
                    Developer = "Playground Games",
                    Price = 1199,
                    Publisher = "Xbox Game Studios", // Додано
                    TrailerUrl = "https://www.youtube.com/embed/FYH9n37B7Yw", // Додано
                    Rating = 9.2m, // Додано
                    IsActive = true, // Додано
                    ExternalId = "58572", // Додано
                    CreatedAt = now, // Додано
                    UpdatedAt = null // Додано
                },

                // 7. Baldurs Gate 3
                new Game
                {
                    Title = "Baldurs Gate 3",
                    ReleaseDate = new DateOnly(2023, 01, 01),
                    CoverImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR8mPl-xVeLXGdV5jCZEQBjnlpHxXUtzBL6-ypExo95M71r2z0vRpyvAu5Z2MGKplzbSn8gP0bgaJIy1w4Es21taAmFXz3b-3_vd98816nWCA",
                    Description = "Зберіть загін і поверніться до Забутих Королівств. На вас чекає історія про дружбу і зраду, виживання та самопожертву, про солодкий поклик абсолютної влади.",
                    Developer = "Larian Studio",
                    Price = 899,
                    Publisher = "Larian Studios", // Додано
                    TrailerUrl = "https://www.youtube.com/embed/Cm47_4g1-gY", // Додано
                    Rating = 9.6m, // Додано
                    IsActive = true, // Додано
                    ExternalId = "3990", // Додано
                    CreatedAt = now, // Додано
                    UpdatedAt = null // Додано
                },

                // 8. The Elder Scrolls V: Skyrim
                new Game
                {
                    Title = "The Elder Scrolls V: Skyrim",
                    ReleaseDate = new DateOnly(2010, 01, 01),
                    CoverImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR8mPl-xVeLXGdV5jCZEQBjnlpHxXUtzBL6-ypExo95M71r2z0vRpyvAu5Z2MGKplzbSn8gP0bgaJIy1w4Es21taAmFXz3b-3_vd98816nWCA",
                    Description = "У грі The Elder Scrolls V: Skyrim Special Edition, що отримала понад 200 нагород «Гра року», на вас чекає дивовижний світ, відтворений з приголомшливою деталізацією. До видання Special Edition увійшли базова гра та доповнення, що додають до неї низку нових можливостей.",
                    Developer = "Bethesda Game Studios",
                    Price = 17,
                    Publisher = "Bethesda Softworks", // Додано
                    TrailerUrl = "https://www.youtube.com/embed/JSRtYHN1K-Q", // Додано
                    Rating = 9.4m, // Додано
                    IsActive = true, // Додано
                    ExternalId = "109", // Додано
                    CreatedAt = now, // Додано
                    UpdatedAt = null // Додано
                },

                // 9. Terraria
                new Game
                {
                    Title = "Terraria",
                    ReleaseDate = new DateOnly(2011, 01, 01),
                    CoverImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcR8mPl-xVeLXGdV5jCZEQBjnlpHxXUtzBL6-ypExo95M71r2z0vRpyvAu5Z2MGKplzbSn8gP0bgaJIy1w4Es21taAmFXz3b-3_vd98816nWCA",
                    Description = "Копайте, боріться, досліджуйте, будуйте! Немає нічого неможливого у цій насиченій подіями пригодницькій грі. Також доступний комплект для чотирьох!",
                    Developer = "Re-Logic",
                    Price = 225,
                    Publisher = "Re-Logic", // Додано
                    TrailerUrl = "https://www.youtube.com/embed/smE1E0bMhS8", // Додано
                    Rating = 8.8m, // Додано
                    IsActive = true, // Додано
                    ExternalId = "415", // Додано
                    CreatedAt = now, // Додано
                    UpdatedAt = null // Додано
                }

                );

                await context.SaveChangesAsync();
            }
        }
    }
}