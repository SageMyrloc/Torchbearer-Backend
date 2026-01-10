using Torchbearer.Domain.Entities;

namespace Torchbearer.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Player player);
}
