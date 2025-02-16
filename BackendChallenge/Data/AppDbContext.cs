using Microsoft.EntityFrameworkCore;
using BackendChallenge.Model;

namespace BackendChallenge.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<LoginRequest> LoginRequest { get; set; }
        public DbSet<RegisterRequest> RegisterRequest { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<UserLike> UserLike { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=app.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginRequest>().HasNoKey();
            modelBuilder.Entity<RegisterRequest>().HasNoKey();
            modelBuilder.Entity<UserLike>().HasKey(ul => new { ul.UserId, ul.BookId });
        }
    }
}
