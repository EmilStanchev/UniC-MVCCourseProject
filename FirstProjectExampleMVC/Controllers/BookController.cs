using FirstProjectExampleMVC.Models;
using FirstProjectExampleMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class BookController : Controller
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View(_service.GetAllBooks());
    }
    [Authorize]

    public IActionResult Create()
    {
        var vm = new CreateBookViewModel
        {
            Authors = _service.GetAuthorsSelectList(),
            Genres = _service.GetGenresSelectList()
        };

        return View(vm);
    }

    [HttpPost]
    [Authorize]

    public IActionResult Create(CreateBookViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Authors = _service.GetAuthorsSelectList();
            vm.Genres = _service.GetGenresSelectList();
            return View(vm);
        }

        var book = new Book
        {
            Title = vm.Title,
            AuthorId = vm.AuthorId
        };

        _service.CreateBook(book, vm.SelectedGenres);

        return RedirectToAction(nameof(Index));
    }
    [Authorize]

    public IActionResult Edit(int id)
    {
        var book = _service.GetBookById(id);
        if (book == null) return NotFound();

        var vm = new EditBookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            AuthorId = book.AuthorId,
            SelectedGenres = book.Genres.Select(g => g.Id).ToList(),
            Authors = _service.GetAuthorsSelectList(),
            Genres = _service.GetGenresSelectList()
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult Edit(EditBookViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Authors = _service.GetAuthorsSelectList();
            vm.Genres = _service.GetGenresSelectList();
            return View(vm);
        }

        var book = new Book
        {
            Id = vm.Id,
            Title = vm.Title,
            AuthorId = vm.AuthorId
        };

        _service.UpdateBook(book, vm.SelectedGenres);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        _service.DeleteBook(id);
        return RedirectToAction(nameof(Index));
    }
}
