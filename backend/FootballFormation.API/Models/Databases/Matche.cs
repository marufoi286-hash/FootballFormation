namespace FootballFormation.API.Models.Databases;

public class Match
{
    public int Id { get; set; }
    public DateTime MatchDate { get; set; }
    public string Opponent { get; set; } = string.Empty;
    public int VenueId { get; set; }
    public Venue? Venue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}