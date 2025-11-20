namespace FirstProjectExampleMVC.Services.Interfaces
{
    using FirstProjectExampleMVC.Models;

    public interface IGenreService
    {
        List<Genre> GetAll();
        Genre? GetById(int id);
        void Create(Genre genre);
        void Update(Genre genre);
        void Delete(int id);
    }

}
