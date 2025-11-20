namespace FirstProjectExampleMVC.Services.Implementation
{
    using FirstProjectExampleMVC.Models;
    using FirstProjectExampleMVC.Services.Interfaces;

    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Genre> GetAll()
        {
            return _context.Genres.ToList();
        }

        public Genre? GetById(int id)
        {
            return _context.Genres.Find(id);
        }

        public void Create(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var g = _context.Genres.Find(id);
            if (g == null) return;

            _context.Genres.Remove(g);
            _context.SaveChanges();
        }
    }

}
