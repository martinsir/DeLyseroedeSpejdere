using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLyseroedeSpejdere.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet() => RedirectToPage("/Activities/Index");
}
