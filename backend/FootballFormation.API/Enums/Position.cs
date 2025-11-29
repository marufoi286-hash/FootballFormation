using System.ComponentModel.DataAnnotations;

namespace FootballFormation.API.Enums;

public enum Position
{
    [Display(Name = "GK")]
    Goalkeeper = 1,
    [Display(Name = "DF")]
    Defender = 2,
    [Display(Name = "MF")]
    Midfielder = 3,
    [Display(Name = "FW")]
    Forward = 4
}