using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Configurations;

public class HexConfiguration : IEntityTypeConfiguration<Hex>
{
    public void Configure(EntityTypeBuilder<Hex> builder)
    {
        builder.HasKey(h => h.Id);

        builder.HasIndex(h => new { h.HexMapId, h.Q, h.R, h.S })
            .IsUnique();

        builder.Property(h => h.Status)
            .HasConversion<int>();

        builder.HasOne(h => h.HexMap)
            .WithMany(m => m.Hexes)
            .HasForeignKey(h => h.HexMapId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.TerrainType)
            .WithMany(t => t.Hexes)
            .HasForeignKey(h => h.TerrainTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
