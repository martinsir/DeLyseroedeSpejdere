namespace DeLyseroedeSpejdere.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Role { get; set; } = "Scout";
    public int Age { get; set; }
    public string Badges { get; set; } = ""; // Kommaadskilte færdighedstegn
}
