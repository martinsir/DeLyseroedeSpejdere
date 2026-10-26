using DeLyseroedeSpejdere.Models;

namespace DeLyseroedeSpejdere.Services;

/// <summary>Pure business rules; database capacity and duplicate checks run in a transaction.</summary>
public static class RegistrationRules
{
    public static RegistrationResult CheckEligibility(User user, Activity activity)
    {
        if (user.Role != "Scout")
            return new(false, "Kun spejdere kan tilmelde sig.");
        if (user.Age < activity.MinAge)
            return new(false, $"Du skal være mindst {activity.MinAge} år.");
        if (!string.IsNullOrWhiteSpace(activity.RequiredBadge) &&
            !(user.Badges ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Any(b => string.Equals(b, activity.RequiredBadge, StringComparison.OrdinalIgnoreCase)))
            return new(false, $"Aktiviteten kræver færdighedstegnet: {activity.RequiredBadge}.");
        return new(true, "Kravene er opfyldt.");
    }
}
