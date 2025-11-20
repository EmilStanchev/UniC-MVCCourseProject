using FirstProjectExampleMVC.Models;
using FirstProjectExampleMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class GenreController : Controller
{
    private readonly IGenreService _service;

    public GenreController(IGenreService service)
    {
        _service = service;
    }
    [Authorize]

    public IActionResult Index() => View(_service.GetAll());
    [Authorize]

    public IActionResult Create() => View();

    [HttpPost]
    [Authorize]

    public IActionResult Create(Genre genre)
    {
        if (!ModelState.IsValid) return View(genre);

        _service.Create(genre);
        return RedirectToAction(nameof(Index));
    }
    [Authorize]

    public IActionResult Edit(int id)
    {
        var genre = _service.GetById(id);
        if (genre == null) return NotFound();
        return View(genre);
    }
    [Authorize]

    [HttpPost]
    public IActionResult Edit(Genre genre)
    {
        if (!ModelState.IsValid) return View(genre);

        _service.Update(genre);
        return RedirectToAction(nameof(Index));
    }
    [Authorize]

    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
