using DeLyseroedeSpejdere.Models;
using DeLyseroedeSpejdere.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLyseroedeSpejdere.Pages.Leader;

[Authorize(Roles = "Leader")]
public class EditModel(IActivityRepository activities) : PageModel
{
    [BindProperty] public ActivityInput Input { get; set; } = new();
    public async Task<IActionResult> OnGetAsync(int id)
    {
        var a = await activities.GetByIdAsync(id);
        if (a == null) return NotFound();
        Input = new ActivityInput
        {
            Title = a.Title, Description = a.Description,
            StartsAt = a.StartsAt, EndsAt = a.EndsAt,
            MaxParticipants = a.MaxParticipants, MinAge = a.MinAge,
            RequiredBadge = a.RequiredBadge
        };
        return Page();
    }
    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid) return Page();
        if (!await activities.UpdateAsync(id, Input))
        {
            ModelState.AddModelError("", "Ændringerne blev ikke gemt. Kontroller, at aktiviteten findes, og at antal pladser ikke er mindre end tilmeldingerne.");
            return Page();
        }
        TempData["Success"] = "Aktiviteten er opdateret.";
        return RedirectToPage("/Leader/Index");
    }
}
