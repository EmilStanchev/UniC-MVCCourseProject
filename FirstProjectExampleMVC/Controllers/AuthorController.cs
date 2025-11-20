using FirstProjectExampleMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AuthorController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var authors = _context.Authors.Include(a => a.Books).ToList();
        return View(authors);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Author author)
    {
        if (ModelState.IsValid)
        {
            _context.Add(author);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(author);
    }

    public IActionResult Edit(int id)
    {
        var author = _context.Authors.Find(id);
        if (author == null) return NotFound();
        return View(author);
    }

    [HttpPost]
    public IActionResult Edit(Author author)
    {
        if (ModelState.IsValid)
        {
            _context.Update(author);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(author);
    }

    public IActionResult Delete(int id)
    {
        var author = _context.Authors.Find(id);
        if (author == null) return NotFound();

        _context.Authors.Remove(author);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
