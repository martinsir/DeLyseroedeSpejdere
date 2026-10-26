using DeLyseroedeSpejdere.Models;
using DeLyseroedeSpejdere.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLyseroedeSpejdere.Pages.Leader;

[Authorize(Roles = "Leader")]
public class ParticipantsModel(IActivityRepository activities, IRegistrationRepository registrations) : PageModel
{
    public string ActivityTitle { get; private set; } = "";
    public List<User> Participants { get; private set; } = new();
    public async Task<IActionResult> OnGetAsync(int id)
    {
        var a = await activities.GetByIdAsync(id);
        if (a == null) return NotFound();
        ActivityTitle = a.Title;
        Participants = await registrations.GetParticipantsAsync(id);
        return Page();
    }
}
