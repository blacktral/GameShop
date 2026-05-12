using GameShop.Models;
using GameShop.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseDefaultServiceProvider(options => options.ValidateScopes = false);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Налаштування паролів тощо, якщо треба
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IGameRepository, EFGameRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("GameShopConnection"));
});

var jwtKey = builder.Configuration["Jwt:Key"] ?? "SecretKey";
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddSingleton(key);
builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<IRawgApiService, RawgApiService>();
builder.Services.AddHttpClient();

// --- БЛОК КОШИКА (НОВЕ) ---
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Доступ до HttpContext
builder.Services.AddScoped<CartService>(); // Реєстрація нашого сервісу
builder.Services.AddDistributedMemoryCache(); // Потрібно для роботи сесій
builder.Services.AddSession(); // Вмикаємо підтримку сесій
// ---------------------------

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/UserAccount/Login";
    options.AccessDeniedPath = "/UserAccount/AccessDenied";
    options.Cookie.Name = "GameShopAuth";
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
    };
});


var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();

app.UseStaticFiles();


app.UseSession();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute("pagination", "Game/Page{page}", new { Controller = "Home", Action = "Home" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

SeedData.EnsurePopulated(app);

app.Run();