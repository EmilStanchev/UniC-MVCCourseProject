using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class CreateBookViewModel
{
    [MaxLength(100,ErrorMessage ="The title should be less than 100 symbols")]
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Please select an author")]
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "Please select at least one genre")]
    public List<int> SelectedGenres { get; set; } = new List<int>();

    public IEnumerable<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
}
