using System.ComponentModel.DataAnnotations;
using DeLyseroedeSpejdere.Models;
using Xunit;

namespace DeLyseroedeSpejdere.Tests;

public class ActivityValidationTests
{
    private static ActivityInput ValidInput() => new()
    {
        Title = "Mad over bål", Description = "Madlavning",
        StartsAt = new DateTime(2026, 10, 29, 9, 0, 0),
        EndsAt = new DateTime(2026, 10, 29, 11, 0, 0),
        MaxParticipants = 10, MinAge = 10
    };

    private static List<ValidationResult> Validate(ActivityInput input)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(input, new ValidationContext(input), results, true);
        return results;
    }

    [Fact]
    public void Activity_WithValidValues_HasNoValidationErrors()
        => Assert.Empty(Validate(ValidInput()));

    [Fact]
    public void Activity_WithEndBeforeStart_IsRejected()
    {
        var input = ValidInput();
        input.EndsAt = input.StartsAt.AddMinutes(-1);
        Assert.Contains(Validate(input), r => r.MemberNames.Contains(nameof(input.EndsAt)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(501)]
    public void Activity_WithInvalidCapacity_IsRejected(int capacity)
    {
        var input = ValidInput();
        input.MaxParticipants = capacity;
        Assert.Contains(Validate(input), r => r.MemberNames.Contains(nameof(input.MaxParticipants)));
    }

    [Fact]
    public void Activity_FreePlaces_IsNeverNegative()
    {
        var activity = new Activity { MaxParticipants = 5, RegisteredCount = 7 };
        Assert.Equal(0, activity.FreePlaces);
    }
}
