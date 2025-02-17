using Microsoft.EntityFrameworkCore;
using BackendChallenge.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BackendChallenge.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) {}
        public DbSet<User> User { get; set; }
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
