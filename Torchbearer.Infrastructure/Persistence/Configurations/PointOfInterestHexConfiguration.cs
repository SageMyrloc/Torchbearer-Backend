using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Configurations;

public class PointOfInterestHexConfiguration : IEntityTypeConfiguration<PointOfInterestHex>
{
    public void Configure(EntityTypeBuilder<PointOfInterestHex> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => new { p.HexMapId, p.HexId, p.PointOfInterestId })
            .IsUnique();

        builder.HasOne(p => p.HexMap)
            .WithMany()
            .HasForeignKey(p => p.HexMapId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Hex)
            .WithMany(h => h.PointOfInterestHexes)
            .HasForeignKey(p => p.HexId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.PointOfInterest)
            .WithMany(poi => poi.PointOfInterestHexes)
            .HasForeignKey(p => p.PointOfInterestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
