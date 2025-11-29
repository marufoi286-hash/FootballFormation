using System.ComponentModel.DataAnnotations.Schema;

namespace FootballFormation.API.Models.Databases;

public class Formation
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public Match? Match { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    [Column(TypeName = "jsonb")]
    public required List<PitchPlayer> PitchData { get; set; } = new();
}