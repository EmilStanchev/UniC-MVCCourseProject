using FirstProjectExampleMVC.Models;
using FirstProjectExampleMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class AuthorController : Controller
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService service)
    {
        _service = service;
    }

    public IActionResult Index() => View(_service.GetAll());

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Author author)
    {
        if (!ModelState.IsValid) return View(author);

        _service.Create(author);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var author = _service.GetById(id);
        if (author == null) return NotFound();
        return View(author);
    }

    [HttpPost]
    public IActionResult Edit(Author author)
    {
        if (!ModelState.IsValid) return View(author);

        _service.Update(author);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
