namespace DeLyseroedeSpejdere.Models;

public class Activity
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public int MaxParticipants { get; set; }
    public int MinAge { get; set; }
    public string RequiredBadge { get; set; } = "";
    public int LeaderId { get; set; }
    public string LeaderName { get; set; } = "";
    public int RegisteredCount { get; set; }
    public int FreePlaces => Math.Max(0, MaxParticipants - RegisteredCount);
}
