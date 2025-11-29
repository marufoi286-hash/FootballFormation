namespace FootballFormation.API.Models.Databases;

public class Venue
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int TeamId { get; set; }
    public Team? Team { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public List<Match> Matches { get; set; } = new();
}