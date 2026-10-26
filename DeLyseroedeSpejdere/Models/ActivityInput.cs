using System.ComponentModel.DataAnnotations;

namespace DeLyseroedeSpejdere.Models;

public class ActivityInput : IValidatableObject
{
    [Required(ErrorMessage = "Angiv et navn.")]
    [StringLength(120)]
    [Display(Name = "Navn")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Angiv en beskrivelse.")]
    [StringLength(2000)]
    [Display(Name = "Beskrivelse")]
    public string Description { get; set; } = "";

    [Display(Name = "Start")]
    public DateTime StartsAt { get; set; } = new(2026, 10, 28, 10, 0, 0);

    [Display(Name = "Slut")]
    public DateTime EndsAt { get; set; } = new(2026, 10, 28, 12, 0, 0);

    [Range(1, 500, ErrorMessage = "Vælg mellem 1 og 500 pladser.")]
    [Display(Name = "Maks. deltagere")]
    public int MaxParticipants { get; set; } = 10;

    [Range(0, 99)]
    [Display(Name = "Minimumsalder")]
    public int MinAge { get; set; }

    [StringLength(80)]
    [Display(Name = "Påkrævet færdighedstegn (valgfrit)")]
    public string? RequiredBadge { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndsAt <= StartsAt)
            yield return new ValidationResult("Sluttidspunkt skal ligge efter starttidspunkt.",
                new[] { nameof(EndsAt) });
    }
}
