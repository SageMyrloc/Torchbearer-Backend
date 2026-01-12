using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Configurations;

public class SessionCharacterConfiguration : IEntityTypeConfiguration<SessionCharacter>
{
    public void Configure(EntityTypeBuilder<SessionCharacter> builder)
    {
        builder.HasKey(sc => sc.Id);

        builder.HasIndex(sc => new { sc.SessionId, sc.CharacterId })
            .IsUnique();

        builder.Property(sc => sc.SessionId)
            .IsRequired();

        builder.Property(sc => sc.CharacterId)
            .IsRequired();

        builder.Property(sc => sc.SignedUpAt)
            .IsRequired();

        builder.HasOne(sc => sc.Session)
            .WithMany(s => s.SessionCharacters)
            .HasForeignKey(sc => sc.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sc => sc.Character)
            .WithMany()
            .HasForeignKey(sc => sc.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
