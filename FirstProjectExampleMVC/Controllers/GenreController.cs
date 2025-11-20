using FirstProjectExampleMVC.Models;
using FirstProjectExampleMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class GenreController : Controller
{
    private readonly IGenreService _service;

    public GenreController(IGenreService service)
    {
        _service = service;
    }

    public IActionResult Index() => View(_service.GetAll());

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Genre genre)
    {
        if (!ModelState.IsValid) return View(genre);

        _service.Create(genre);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var genre = _service.GetById(id);
        if (genre == null) return NotFound();
        return View(genre);
    }

    [HttpPost]
    public IActionResult Edit(Genre genre)
    {
        if (!ModelState.IsValid) return View(genre);

        _service.Update(genre);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
