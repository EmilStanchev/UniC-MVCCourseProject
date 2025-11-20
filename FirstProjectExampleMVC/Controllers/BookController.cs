using FirstProjectExampleMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;
    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books1 = _context.Books.ToList();
        var books = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genres)
            .ToList();
        return View(books);
    }

    public IActionResult Create()
    {
        var vm = new CreateBookViewModel
        {
            Authors = _context.Authors
                .OrderBy(a => a.Name)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name })
                .ToList(),
            Genres = _context.Genres
                .OrderBy(g => g.Name)
                .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                .ToList()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateBookViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Authors = _context.Authors
                .OrderBy(a => a.Name)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name })
                .ToList();
            vm.Genres = _context.Genres
                .OrderBy(g => g.Name)
                .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                .ToList();
            return View(vm);
        }

        var book = new Book
        {
            Title = vm.Title,
            AuthorId = vm.AuthorId
        };

        if (vm.SelectedGenres != null && vm.SelectedGenres.Any())
        {
            var genres = _context.Genres
                .Where(g => vm.SelectedGenres.Contains(g.Id))
                .ToList();

            foreach (var genre in genres)
                book.Genres.Add(genre);
        }

        _context.Books.Add(book);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    // 🔹 GET: Books/Edit/5
    public IActionResult Edit(int id)
    {
        var book = _context.Books
            .Include(b => b.Genres)
            .FirstOrDefault(b => b.Id == id);

        if (book == null)
            return NotFound();

        var vm = new EditBookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            AuthorId = book.AuthorId,
            SelectedGenres = book.Genres.Select(g => g.Id).ToList(),
            Authors = _context.Authors
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name })
                .ToList(),
            Genres = _context.Genres
                .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                .ToList()
        };

        return View(vm);
    }

    // 🔹 POST: Books/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EditBookViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Authors = _context.Authors
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name })
                .ToList();
            vm.Genres = _context.Genres
                .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                .ToList();
            return View(vm);
        }

        var book = _context.Books
            .Include(b => b.Genres)
            .FirstOrDefault(b => b.Id == vm.Id);

        if (book == null)
            return NotFound();

        book.Title = vm.Title;
        book.AuthorId = vm.AuthorId;

        book.Genres.Clear();
        if (vm.SelectedGenres != null && vm.SelectedGenres.Any())
        {
            var genres = _context.Genres
                .Where(g => vm.SelectedGenres.Contains(g.Id))
                .ToList();

            foreach (var genre in genres)
                book.Genres.Add(genre);
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }



    public IActionResult Delete(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return NotFound();

        _context.Books.Remove(book);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
