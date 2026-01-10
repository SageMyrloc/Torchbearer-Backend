using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Configurations;

public class HexMapConfiguration : IEntityTypeConfiguration<HexMap>
{
    public void Configure(EntityTypeBuilder<HexMap> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.CreatedAt)
            .IsRequired();

        builder.HasOne(h => h.Character)
            .WithOne(c => c.HexMap)
            .HasForeignKey<HexMap>(h => h.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
