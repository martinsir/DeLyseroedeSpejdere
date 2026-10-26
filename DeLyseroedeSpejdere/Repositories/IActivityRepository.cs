using DeLyseroedeSpejdere.Models;
namespace DeLyseroedeSpejdere.Repositories;

public interface IActivityRepository
{
    Task<List<Activity>> GetAllAsync();
    Task<Activity?> GetByIdAsync(int id);
    Task<int> InsertAsync(ActivityInput activity, int leaderId);
    Task<bool> UpdateAsync(int id, ActivityInput activity);
    Task<bool> DeleteAsync(int id);
}
