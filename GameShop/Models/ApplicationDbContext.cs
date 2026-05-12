using Microsoft.EntityFrameworkCore;

namespace GameShop.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 1. Властивості DbSet для всіх моделей
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Game { get; set; } // (Примітка: зазвичай називають Games у множині, але я залишив як у тебе)
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<UserLibrary> UserLibraries { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Налаштування композитних первинних ключів (виправлення помилки "requires a primary key")

            // 1. Композитний ключ для GameGenre (Гра + Жанр)
            modelBuilder.Entity<GameGenre>()
                .HasKey(gg => new { gg.GameId, gg.GenresId });

            // 2. Композитний ключ для UserLibrary (Користувач + Гра)
            modelBuilder.Entity<UserLibrary>()
                .HasKey(ul => new { ul.UserId, ul.GameId });


            // Додаткові налаштування: забезпечення унікальності Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}