using FootballFormation.API.Enums;

namespace FootballFormation.API.Models.Databases;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Position Position { get; set; }
    public int TeamId { get; set; }
    public Team? Team { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}