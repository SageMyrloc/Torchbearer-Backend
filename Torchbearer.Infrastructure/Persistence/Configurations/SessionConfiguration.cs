using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.Property(s => s.ScheduledAt)
            .IsRequired();

        builder.Property(s => s.GameMasterId)
            .IsRequired();

        builder.Property(s => s.Status)
            .IsRequired();

        builder.Property(s => s.GoldReward)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.ExperienceReward)
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.Property(s => s.CompletedAt);

        builder.HasOne(s => s.GameMaster)
            .WithMany()
            .HasForeignKey(s => s.GameMasterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
