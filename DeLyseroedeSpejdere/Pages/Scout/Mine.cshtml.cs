using System.Security.Claims;
using DeLyseroedeSpejdere.Models;
using DeLyseroedeSpejdere.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLyseroedeSpejdere.Pages.Scout;

[Authorize(Roles = "Scout")]
public class MineModel(IRegistrationRepository registrations) : PageModel
{
    public List<Activity> Activities { get; private set; } = new();
    public async Task<IActionResult> OnGetAsync()
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)) return Forbid();
        Activities = await registrations.GetMineAsync(userId);
        return Page();
    }
    public async Task<IActionResult> OnPostCancelAsync(int id)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)) return Forbid();
        TempData[await registrations.CancelAsync(userId, id) ? "Success" : "Error"] =
            "Tilmelding opdateret.";
        return RedirectToPage();
    }
}
