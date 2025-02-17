using Microsoft.EntityFrameworkCore;
using BackendChallenge.Model;

namespace BackendChallenge.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<UserLike> UserLike { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=app.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLike>().HasKey(ul => new { ul.UserId, ul.BookId });
            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.User)
                .WithMany()
                .HasForeignKey(ul => ul.UserId);

            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.Book)
                .WithMany()
                .HasForeignKey(ul => ul.BookId);
        }
    }
}
