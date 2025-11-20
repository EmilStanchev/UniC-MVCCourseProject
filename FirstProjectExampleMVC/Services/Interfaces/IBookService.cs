namespace FirstProjectExampleMVC.Services.Interfaces
{
    using FirstProjectExampleMVC.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
        void CreateBook(Book book, List<int>? genreIds);
        void UpdateBook(Book book, List<int>? genreIds);
        void DeleteBook(int id);

        List<SelectListItem> GetAuthorsSelectList();
        List<SelectListItem> GetGenresSelectList();
    }

}
