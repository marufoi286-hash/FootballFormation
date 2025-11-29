namespace FootballFormation.API.Models.Databases;

public class PitchPlayer
{
    public int? PlayerId { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
}