using DeLyseroedeSpejdere.Models;
namespace DeLyseroedeSpejdere.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task InsertAsync(User user);
}
