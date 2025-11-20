using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class BookFormViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Please select an author")]
    public int AuthorId { get; set; }

    public List<int> SelectedGenres { get; set; } = new List<int>();

    public IEnumerable<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
}
