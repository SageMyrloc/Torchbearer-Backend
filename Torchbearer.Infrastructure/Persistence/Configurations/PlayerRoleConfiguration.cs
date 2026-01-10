using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Configurations;

public class PlayerRoleConfiguration : IEntityTypeConfiguration<PlayerRole>
{
    public void Configure(EntityTypeBuilder<PlayerRole> builder)
    {
        builder.HasKey(pr => pr.Id);

        builder.HasIndex(pr => new { pr.PlayerId, pr.RoleId })
            .IsUnique();

        builder.HasOne(pr => pr.Player)
            .WithMany(p => p.PlayerRoles)
            .HasForeignKey(pr => pr.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pr => pr.Role)
            .WithMany(r => r.PlayerRoles)
            .HasForeignKey(pr => pr.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
