using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Torchbearer.Application.Interfaces;
using Torchbearer.Infrastructure.Authentication;
using Torchbearer.Infrastructure.Persistence;
using Torchbearer.Infrastructure.Persistence.Repositories;

namespace Torchbearer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TorchbearerDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();
        services.AddScoped<IHexMapRepository, HexMapRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<ISessionCharacterRepository, SessionCharacterRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
