namespace FirstProjectExampleMVC.Services.Implementation
{
    using FirstProjectExampleMVC.Models;
    using FirstProjectExampleMVC.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .ToList();
        }

        public Book? GetBookById(int id)
        {
            return _context.Books
                .Include(b => b.Genres)
                .FirstOrDefault(b => b.Id == id);
        }

        public void CreateBook(Book book, List<int>? genreIds)
        {
            if (genreIds != null && genreIds.Any())
            {
                var genres = _context.Genres.Where(g => genreIds.Contains(g.Id)).ToList();
                foreach (var g in genres) book.Genres.Add(g);
            }

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book updated, List<int>? genreIds)
        {
            var book = _context.Books
                .Include(b => b.Genres)
                .FirstOrDefault(b => b.Id == updated.Id);

            if (book == null) return;

            book.Title = updated.Title;
            book.AuthorId = updated.AuthorId;

            book.Genres.Clear();

            if (genreIds != null && genreIds.Any())
            {
                var genres = _context.Genres.Where(g => genreIds.Contains(g.Id)).ToList();
                foreach (var g in genres) book.Genres.Add(g);
            }

            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return;

            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public List<SelectListItem> GetAuthorsSelectList()
        {
            return _context.Authors
                .OrderBy(a => a.Name)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();
        }

        public List<SelectListItem> GetGenresSelectList()
        {
            return _context.Genres
                .OrderBy(g => g.Name)
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                })
                .ToList();
        }
    }

}
