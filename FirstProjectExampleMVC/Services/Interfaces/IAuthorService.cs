namespace FirstProjectExampleMVC.Services.Interfaces
{
    using FirstProjectExampleMVC.Models;

    public interface IAuthorService
    {
        List<Author> GetAll();
        Author? GetById(int id);
        void Create(Author author);
        void Update(Author author);
        void Delete(int id);
    }

}
