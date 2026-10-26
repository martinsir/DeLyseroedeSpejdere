using DeLyseroedeSpejdere.Models;
using MySqlConnector;

namespace DeLyseroedeSpejdere.Repositories;

public class UserRepository(DbConnectionFactory factory) : IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username)
    {
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(
            "SELECT Id, Username, PasswordHash, FullName, Role, Age, Badges FROM Users WHERE Username=@Username", conn);
        cmd.Parameters.AddWithValue("@Username", username);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;
        return Map(reader);
    }

    public async Task InsertAsync(User user)
    {
        await using var conn = factory.Create();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(
            "INSERT INTO Users (Username,PasswordHash,FullName,Role,Age,Badges) VALUES (@Username,@Hash,@FullName,@Role,@Age,@Badges)", conn);
        cmd.Parameters.AddWithValue("@Username", user.Username);
        cmd.Parameters.AddWithValue("@Hash", user.PasswordHash);
        cmd.Parameters.AddWithValue("@FullName", user.FullName);
        cmd.Parameters.AddWithValue("@Role", user.Role);
        cmd.Parameters.AddWithValue("@Age", user.Age);
        cmd.Parameters.AddWithValue("@Badges", user.Badges);
        await cmd.ExecuteNonQueryAsync();
    }

    private static User Map(MySqlDataReader r) => new()
    {
        Id = r.GetInt32("Id"),
        Username = r.GetString("Username"),
        PasswordHash = r.GetString("PasswordHash"),
        FullName = r.GetString("FullName"),
        Role = r.GetString("Role"),
        Age = r.GetInt32("Age"),
        Badges = r.GetString("Badges")
    };
}
