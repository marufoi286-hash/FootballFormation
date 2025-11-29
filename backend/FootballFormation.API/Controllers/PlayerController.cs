using FootballFormation.API.Dtos;
using FootballFormation.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballFormation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PlayerDto>>> GetAll()
    {
        var players = await _playerService.GetAllAsync();
        return Ok(players);
    }
}
