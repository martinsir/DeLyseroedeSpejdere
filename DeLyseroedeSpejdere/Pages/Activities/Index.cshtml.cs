using System.Security.Claims;
using DeLyseroedeSpejdere.Models;
using DeLyseroedeSpejdere.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLyseroedeSpejdere.Pages.Activities;

public class IndexModel(IActivityRepository activities, IRegistrationRepository registrations) : PageModel
{
    public List<Activity> Activities { get; private set; } = new();
    public async Task OnGetAsync() => Activities = await activities.GetAllAsync();

    public async Task<IActionResult> OnPostJoinAsync(int id)
    {
        if (User.Identity?.IsAuthenticated != true) return Challenge();
        if (!User.IsInRole("Scout")) return Forbid();
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)) return Forbid();
        var result = await registrations.JoinAsync(userId, id);
        TempData[result.Success ? "Success" : "Error"] = result.Message;
        return RedirectToPage();
    }
}
