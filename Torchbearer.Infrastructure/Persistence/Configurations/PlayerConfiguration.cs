using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.PasswordHash)
            .IsRequired();

        builder.Property(p => p.MaxCharacterSlots)
            .IsRequired()
            .HasDefaultValue(1);

        builder.HasIndex(p => p.Username)
            .IsUnique();
    }
}
