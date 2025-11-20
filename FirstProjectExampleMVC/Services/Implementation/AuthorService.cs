namespace FirstProjectExampleMVC.Services.Implementation
{
    using FirstProjectExampleMVC.Models;
    using FirstProjectExampleMVC.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Author> GetAll()
        {
            return _context.Authors.Include(a => a.Books).ToList();
        }

        public Author? GetById(int id)
        {
            return _context.Authors.Find(id);
        }

        public void Create(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void Update(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = _context.Authors.Find(id);
            if (author == null) return;

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }

}
