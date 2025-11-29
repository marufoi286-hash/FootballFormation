using FootballFormation.API.Models.Databases;
using Microsoft.EntityFrameworkCore;

namespace FootballFormation.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Formation> Formation { get; set; }
    public DbSet<Match> Matche { get; set; }
    public DbSet<Player> Player { get; set; }
    public DbSet<Team> Team { get; set; }
    public DbSet<Venue> Venue { get; set; }
}