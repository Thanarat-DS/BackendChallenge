using Microsoft.EntityFrameworkCore;
using BackendChallenge.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BackendChallenge.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Book> Book { get; set; }
        public DbSet<UserLike> UserLike { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=app.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLike>().HasKey(ul => new { ul.UserId, ul.BookId });

            // relationship between UserLike and User
            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.User)
                .WithMany()
                .HasForeignKey(ul => ul.UserId);

            // relationship between UserLike and Book
            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.Book)
                .WithMany()
                .HasForeignKey(ul => ul.BookId);

        }
    }
}
