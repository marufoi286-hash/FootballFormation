namespace FootballFormation.API.Models.Databases;
public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LoginId { get; set;} = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public List<Player> Players { get; set; } = new();
    public List<Venue> Venues { get; set; } = new();
    public List<Match> Matches { get; set; } = new();
}