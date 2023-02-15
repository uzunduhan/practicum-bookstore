using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class BookStoreDbContext: DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options): base(options)
        {
        }

        public DbSet<Book> Books{get; set;}
        public DbSet<Genre> Genres{get; set;}
        public DbSet<Author> Authors {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .HasOne(x=>x.Author)
            .WithMany(p=>p.Books)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}