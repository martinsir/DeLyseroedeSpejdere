using DeLyseroedeSpejdere.Models;
using DeLyseroedeSpejdere.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLyseroedeSpejdere.Pages.Leader;

[Authorize(Roles = "Leader")]
public class IndexModel(IActivityRepository activities) : PageModel
{
    public List<Activity> Activities { get; private set; } = new();
    public async Task OnGetAsync() => Activities = await activities.GetAllAsync();
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var success = await activities.DeleteAsync(id);
        TempData[success ? "Success" : "Error"] = success ? "Aktiviteten er slettet." : "Aktiviteten blev ikke fundet.";
        return RedirectToPage();
    }
}
