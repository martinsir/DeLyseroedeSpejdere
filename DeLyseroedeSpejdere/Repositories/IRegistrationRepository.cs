using DeLyseroedeSpejdere.Models;
namespace DeLyseroedeSpejdere.Repositories;

public interface IRegistrationRepository
{
    Task<RegistrationResult> JoinAsync(int userId, int activityId);
    Task<bool> CancelAsync(int userId, int activityId);
    Task<List<Activity>> GetMineAsync(int userId);
    Task<List<User>> GetParticipantsAsync(int activityId);
}
