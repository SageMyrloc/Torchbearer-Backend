using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Configurations;

public class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ImageFileName)
            .HasMaxLength(255);

        builder.Property(c => c.Status)
            .IsRequired();

        builder.Property(c => c.Gold)
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.ExperiencePoints)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.ApprovedAt);

        builder.HasOne(c => c.Player)
            .WithMany(p => p.Characters)
            .HasForeignKey(c => c.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.HexMap)
            .WithOne(h => h.Character)
            .HasForeignKey<HexMap>(h => h.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
