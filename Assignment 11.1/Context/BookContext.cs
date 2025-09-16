using Microsoft.EntityFrameworkCore;
using Assignment_11._1.Models;

namespace Assignment_11._1.Context
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated(); // Database created if does not exist
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(GetBooks()); // data seeding
            base.OnModelCreating(modelBuilder);
        }
        private Book[] GetBooks()
        {
            return new Book[]
            {
                new Book { ISBN = "1001", Title = "The Art of Code", Author = "Ada Lovelace", Description = "A deep dive into the elegance and creativity behind programming languages." },
                new Book { ISBN = "1002", Title = "Digital Dreams", Author = "Alan Turing", Description = "Exploring the philosophical and mathematical foundations of artificial intelligence." },
                new Book { ISBN = "1003", Title = "C# Chronicles", Author = "Grace Hopper", Description = "A practical guide to mastering C# with real-world examples and expert insights." }
            };

        }
    }
}
