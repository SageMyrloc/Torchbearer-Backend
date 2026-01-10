using Microsoft.EntityFrameworkCore;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence;

public class TorchbearerDbContext : DbContext
{
    public TorchbearerDbContext(DbContextOptions<TorchbearerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<PlayerRole> PlayerRoles { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<HexMap> HexMaps { get; set; }
    public DbSet<TerrainType> TerrainTypes { get; set; }
    public DbSet<Hex> Hexes { get; set; }
    public DbSet<PointOfInterest> PointsOfInterest { get; set; }
    public DbSet<PointOfInterestHex> PointOfInterestHexes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TorchbearerDbContext).Assembly);
    }
}
