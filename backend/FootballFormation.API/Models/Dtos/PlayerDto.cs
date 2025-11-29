using FootballFormation.API.Models.Databases;

namespace FootballFormation.API.Dtos;

public class PlayerDto
{

    public PlayerDto()
    {
    }

    public PlayerDto(Player player)
    {
        Id = player.Id;
        Name = player.Name;
        Position = player.Position.ToString();
        TeamId = player.TeamId;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int TeamId { get; set; }
}