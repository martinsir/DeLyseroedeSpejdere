using DeLyseroedeSpejdere.Services;
using DeLyseroedeSpejdere.Models;
using MySqlConnector;

namespace DeLyseroedeSpejdere.Repositories;

public class RegistrationRepository(DbConnectionFactory factory) : IRegistrationRepository
{
    public async Task<RegistrationResult> JoinAsync(int userId, int activityId)
    {
        await using var conn = factory.Create();
        await conn.OpenAsync();
        // Lås aktivitetsrækken for at undgå overbooking ved samtidige tilmeldinger.
        await using var tx = await conn.BeginTransactionAsync();
        await using var info = new MySqlCommand(@"
SELECT a.MaxParticipants, a.MinAge, a.RequiredBadge, u.Age, u.Badges, u.Role
FROM Activities a CROSS JOIN Users u
WHERE a.Id=@ActivityId AND u.Id=@UserId FOR UPDATE;", conn, tx);
        info.Parameters.AddWithValue("@ActivityId", activityId);
        info.Parameters.AddWithValue("@UserId", userId);
        int max, minAge, age;
        string badge, badges, role;
        await using (var reader = await info.ExecuteReaderAsync())
        {
            if (!await reader.ReadAsync())
            {
                await reader.DisposeAsync();
                await tx.RollbackAsync();
                return new(false, "Aktiviteten eller brugeren blev ikke fundet.");
            }
            max = reader.GetInt32("MaxParticipants");
            minAge = reader.GetInt32("MinAge");
            age = reader.GetInt32("Age");
            badge = reader.GetString("RequiredBadge");
            badges = reader.GetString("Badges");
            role = reader.GetString("Role");
        }
        var eligibility = RegistrationRules.CheckEligibility(
            new User { Role = role, Age = age, Badges = badges },
            new Activity { MinAge = minAge, RequiredBadge = badge });
        if (!eligibility.Success) return await Reject(eligibility.Message);

        await using var countCmd = new MySqlCommand(@"
SELECT COUNT(*) FROM Registrations WHERE ActivityId=@ActivityId;", conn, tx);
        countCmd.Parameters.AddWithValue("@ActivityId", activityId);
        var count = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
        if (count >= max) return await Reject("Der er ikke flere ledige pladser.");

        await using var duplicate = new MySqlCommand(@"
SELECT COUNT(*) FROM Registrations WHERE ActivityId=@ActivityId AND UserId=@UserId;", conn, tx);
        duplicate.Parameters.AddWithValue("@ActivityId", activityId);
        duplicate.Parameters.AddWithValue("@UserId", userId);
        if (Convert.ToInt32(await duplicate.ExecuteScalarAsync()) > 0)
            return await Reject("Du er allerede tilmeldt aktiviteten.");

        await using var insert = new MySqlCommand(@"
INSERT INTO Registrations (ActivityId, UserId, RegisteredAt)
VALUES (@ActivityId,@UserId,UTC_TIMESTAMP());", conn, tx);
        insert.Parameters.AddWithValue("@ActivityId", activityId);
        insert.Parameters.AddWithValue("@UserId", userId);
        await insert.ExecuteNonQueryAsync();
        await tx.CommitAsync();
        return new(true, "Du er nu tilmeldt aktiviteten.");

        async Task<RegistrationResult> Reject(string message)
        {
            await tx.RollbackAsync();
            return new(false, message);
        }
    }

    public async Task<bool> CancelAsync(int userId, int activityId)
    {
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(
            "DELETE FROM Registrations WHERE ActivityId=@ActivityId AND UserId=@UserId", conn);
        cmd.Parameters.AddWithValue("@ActivityId", activityId);
        cmd.Parameters.AddWithValue("@UserId", userId);
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    public async Task<List<Activity>> GetMineAsync(int userId)
    {
        var result = new List<Activity>();
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(@"
SELECT a.Id,a.Title,a.Description,a.StartsAt,a.EndsAt,a.MaxParticipants,a.MinAge,
       a.RequiredBadge,a.LeaderId,leader.FullName AS LeaderName,
       (SELECT COUNT(*) FROM Registrations r2 WHERE r2.ActivityId=a.Id) AS RegisteredCount
FROM Registrations reg JOIN Activities a ON reg.ActivityId=a.Id
JOIN Users leader ON a.LeaderId=leader.Id
WHERE reg.UserId=@UserId ORDER BY a.StartsAt;", conn);
        cmd.Parameters.AddWithValue("@UserId", userId);
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) result.Add(MapActivity(r));
        return result;
    }

    public async Task<List<User>> GetParticipantsAsync(int activityId)
    {
        var result = new List<User>();
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(@"
SELECT u.Id,u.Username,u.FullName,u.Age,u.Badges
FROM Registrations reg JOIN Users u ON reg.UserId=u.Id
WHERE reg.ActivityId=@ActivityId ORDER BY u.FullName;", conn);
        cmd.Parameters.AddWithValue("@ActivityId", activityId);
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) result.Add(new User
        {
            Id = r.GetInt32("Id"), Username = r.GetString("Username"),
            FullName = r.GetString("FullName"), Age = r.GetInt32("Age"),
            Badges = r.GetString("Badges")
        });
        return result;
    }

    private static Activity MapActivity(MySqlDataReader r) => new()
    {
        Id = r.GetInt32("Id"), Title = r.GetString("Title"),
        Description = r.GetString("Description"), StartsAt = r.GetDateTime("StartsAt"),
        EndsAt = r.GetDateTime("EndsAt"), MaxParticipants = r.GetInt32("MaxParticipants"),
        MinAge = r.GetInt32("MinAge"), RequiredBadge = r.GetString("RequiredBadge"),
        LeaderId = r.GetInt32("LeaderId"), LeaderName = r.GetString("LeaderName"),
        RegisteredCount = Convert.ToInt32(r.GetValue(r.GetOrdinal("RegisteredCount")))
    };
}
