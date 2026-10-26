using MySqlConnector;

namespace DeLyseroedeSpejdere.Repositories;

public class DbConnectionFactory
{
    private readonly string _connectionString;
    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySql")
            ?? throw new InvalidOperationException("ConnectionStrings:MySql mangler.");
    }
    public MySqlConnection Create() => new(_connectionString);
}
