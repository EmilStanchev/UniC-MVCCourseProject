using FirstProjectExampleMVC.Models;
using FirstProjectExampleMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AuthorController : Controller
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService service)
    {
        _service = service;
    }
    [Authorize]

    public IActionResult Index() => View(_service.GetAll());

    [Authorize]

    public IActionResult Create() => View();

    [HttpPost]
    [Authorize]

    public IActionResult Create(Author author)
    {
        if (!ModelState.IsValid) return View(author);

        _service.Create(author);
        return RedirectToAction(nameof(Index));
    }
    [Authorize]

    public IActionResult Edit(int id)
    {
        var author = _service.GetById(id);
        if (author == null) return NotFound();
        return View(author);
    }

    [HttpPost]
    [Authorize]

    public IActionResult Edit(Author author)
    {
        if (!ModelState.IsValid) return View(author);

        _service.Update(author);
        return RedirectToAction(nameof(Index));
    }
    [Authorize]

    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
