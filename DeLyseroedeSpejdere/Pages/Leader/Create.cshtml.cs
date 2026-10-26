using System.Security.Claims;
using DeLyseroedeSpejdere.Models;
using DeLyseroedeSpejdere.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLyseroedeSpejdere.Pages.Leader;

[Authorize(Roles = "Leader")]
public class CreateModel(IActivityRepository activities) : PageModel
{
    [BindProperty] public ActivityInput Input { get; set; } = new();
    public void OnGet() { }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var leaderId)) return Forbid();
        await activities.InsertAsync(Input, leaderId);
        TempData["Success"] = "Aktiviteten er oprettet.";
        return RedirectToPage("/Leader/Index");
    }
}
