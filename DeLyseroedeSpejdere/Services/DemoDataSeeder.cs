using DeLyseroedeSpejdere.Models;
using DeLyseroedeSpejdere.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DeLyseroedeSpejdere.Services;

public class DemoDataSeeder(
    IUserRepository users, IActivityRepository activities, IPasswordHasher<User> hasher)
{
    public async Task SeedAsync()
    {
        await EnsureUser("leder", "Lejr2026!", "Lejrleder Anne", "Leader", 35, "Førstehjælp");
        await EnsureUser("spejder", "Lejr2026!", "Spejder Emil", "Scout", 13, "Bål,Førstehjælp");
        await EnsureUser("junior", "Lejr2026!", "Spejder Sofie", "Scout", 10, "");
        var leader = await users.GetByUsernameAsync("leder")
            ?? throw new InvalidOperationException("Demo-leder mangler.");
        if ((await activities.GetAllAsync()).Count == 0)
        {
            await activities.InsertAsync(new ActivityInput
            {
                Title = "Mad over bål", Description = "Lær at lave mad over et sikkert bål.",
                StartsAt = new DateTime(2026, 10, 28, 10, 0, 0),
                EndsAt = new DateTime(2026, 10, 28, 12, 0, 0),
                MaxParticipants = 8, MinAge = 10
            }, leader.Id);
            await activities.InsertAsync(new ActivityInput
            {
                Title = "Ridning på gården", Description = "Besøg gården og prøv ridning med en leder.",
                StartsAt = new DateTime(2026, 10, 29, 9, 0, 0),
                EndsAt = new DateTime(2026, 10, 29, 11, 0, 0),
                MaxParticipants = 5, MinAge = 12
            }, leader.Id);
            await activities.InsertAsync(new ActivityInput
            {
                Title = "Førstehjælp i naturen", Description = "Øvelser for spejdere med førstehjælpstegn.",
                StartsAt = new DateTime(2026, 10, 29, 13, 0, 0),
                EndsAt = new DateTime(2026, 10, 29, 15, 0, 0),
                MaxParticipants = 12, MinAge = 11, RequiredBadge = "Førstehjælp"
            }, leader.Id);
        }
    }

    private async Task EnsureUser(string username, string password, string fullName,
        string role, int age, string badges)
    {
        if (await users.GetByUsernameAsync(username) != null) return;
        var user = new User
        {
            Username = username, FullName = fullName, Role = role, Age = age, Badges = badges
        };
        user.PasswordHash = hasher.HashPassword(user, password);
        await users.InsertAsync(user);
    }
}
