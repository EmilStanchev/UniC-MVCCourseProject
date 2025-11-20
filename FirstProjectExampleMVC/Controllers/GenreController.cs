using FirstProjectExampleMVC.Models;
using Microsoft.AspNetCore.Mvc;

public class GenreController : Controller
{
    private readonly ApplicationDbContext _context;

    public GenreController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View(_context.Genres.ToList());

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Genre genre)
    {
        if (ModelState.IsValid)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    public IActionResult Edit(int id)
    {
        var genre = _context.Genres.Find(id);
        if (genre == null) return NotFound();
        return View(genre);
    }

    [HttpPost]
    public IActionResult Edit(Genre genre)
    {
        if (ModelState.IsValid)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    public IActionResult Delete(int id)
    {
        var genre = _context.Genres.Find(id);
        if (genre == null) return NotFound();
        _context.Genres.Remove(genre);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
