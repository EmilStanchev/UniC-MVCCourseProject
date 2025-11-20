using FirstProjectExampleMVC.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .UsingEntity(j => j.ToTable("BookGenres"));

        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "J.K. Rowling" },
            new Author { Id = 2, Name = "George R.R. Martin" },
            new Author { Id = 3, Name = "J.R.R. Tolkien" }
        );

        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Fantasy" },
            new Genre { Id = 2, Name = "Adventure" },
            new Genre { Id = 3, Name = "Mystery" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", AuthorId = 1 },
            new Book { Id = 2, Title = "A Game of Thrones", AuthorId = 2 },
            new Book { Id = 3, Title = "The Hobbit", AuthorId = 3 }
        );
    }
}
