using DeLyseroedeSpejdere.Models;
using MySqlConnector;

namespace DeLyseroedeSpejdere.Repositories;

public class ActivityRepository(DbConnectionFactory factory) : IActivityRepository
{
    private const string BaseSelect = @"
SELECT a.Id, a.Title, a.Description, a.StartsAt, a.EndsAt, a.MaxParticipants,
       a.MinAge, a.RequiredBadge, a.LeaderId, u.FullName AS LeaderName,
       (SELECT COUNT(*) FROM Registrations r WHERE r.ActivityId=a.Id) AS RegisteredCount
FROM Activities a JOIN Users u ON a.LeaderId=u.Id";

    public async Task<List<Activity>> GetAllAsync()
    {
        var result = new List<Activity>();
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(BaseSelect + " ORDER BY a.StartsAt, a.Id", conn);
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) result.Add(Map(r));
        return result;
    }

    public async Task<Activity?> GetByIdAsync(int id)
    {
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(BaseSelect + " WHERE a.Id=@Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await using var r = await cmd.ExecuteReaderAsync();
        return await r.ReadAsync() ? Map(r) : null;
    }

    public async Task<int> InsertAsync(ActivityInput a, int leaderId)
    {
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(@"
INSERT INTO Activities (Title,Description,StartsAt,EndsAt,MaxParticipants,MinAge,RequiredBadge,LeaderId)
VALUES (@Title,@Description,@Starts,@Ends,@Max,@Age,@Badge,@Leader);", conn);
        AddParams(cmd, a);
        cmd.Parameters.AddWithValue("@Leader", leaderId);
        await cmd.ExecuteNonQueryAsync();
        return (int)cmd.LastInsertedId;
    }

    public async Task<bool> UpdateAsync(int id, ActivityInput a)
    {
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(@"
UPDATE Activities
SET Title=@Title, Description=@Description, StartsAt=@Starts, EndsAt=@Ends,
    MaxParticipants=@Max, MinAge=@Age, RequiredBadge=@Badge
WHERE Id=@Id AND @Max >= (SELECT COUNT(*) FROM Registrations WHERE ActivityId=@Id);", conn);
        AddParams(cmd, a);
        cmd.Parameters.AddWithValue("@Id", id);
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand("DELETE FROM Activities WHERE Id=@Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    private static void AddParams(MySqlCommand cmd, ActivityInput a)
    {
        cmd.Parameters.AddWithValue("@Title", a.Title.Trim());
        cmd.Parameters.AddWithValue("@Description", a.Description.Trim());
        cmd.Parameters.AddWithValue("@Starts", a.StartsAt);
        cmd.Parameters.AddWithValue("@Ends", a.EndsAt);
        cmd.Parameters.AddWithValue("@Max", a.MaxParticipants);
        cmd.Parameters.AddWithValue("@Age", a.MinAge);
        cmd.Parameters.AddWithValue("@Badge", (a.RequiredBadge ?? "").Trim());
    }

    private static Activity Map(MySqlDataReader r) => new()
    {
        Id = r.GetInt32("Id"),
        Title = r.GetString("Title"),
        Description = r.GetString("Description"),
        StartsAt = r.GetDateTime("StartsAt"),
        EndsAt = r.GetDateTime("EndsAt"),
        MaxParticipants = r.GetInt32("MaxParticipants"),
        MinAge = r.GetInt32("MinAge"),
        RequiredBadge = r.GetString("RequiredBadge"),
        LeaderId = r.GetInt32("LeaderId"),
        LeaderName = r.GetString("LeaderName"),
        RegisteredCount = Convert.ToInt32(r.GetValue(r.GetOrdinal("RegisteredCount")))
    };
}
