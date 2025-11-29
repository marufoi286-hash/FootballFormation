using FootballFormation.API.Data;
using FootballFormation.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FootballFormation.API.Services;

public interface IPlayerService
{
    Task<List<PlayerDto>> GetAllAsync();
}

public class PlayerService : IPlayerService
{
    private readonly ApplicationDbContext _context;

    public PlayerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlayerDto>> GetAllAsync()
    {
        var players = await _context.Player
            .AsNoTracking()
            .Select(players => new PlayerDto(players))
            .ToListAsync();

        return players;
    }
}