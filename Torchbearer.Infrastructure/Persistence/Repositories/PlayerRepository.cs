using Microsoft.EntityFrameworkCore;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly TorchbearerDbContext _context;

    public PlayerRepository(TorchbearerDbContext context)
    {
        _context = context;
    }

    public async Task<Player?> GetByIdAsync(int id)
    {
        return await _context.Players
            .Include(p => p.PlayerRoles)
                .ThenInclude(pr => pr.Role)
            .Include(p => p.Characters)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Player?> GetByUsernameAsync(string username)
    {
        return await _context.Players
            .Include(p => p.PlayerRoles)
                .ThenInclude(pr => pr.Role)
            .FirstOrDefaultAsync(p => p.Username == username);
    }

    public async Task<IEnumerable<Player>> GetAllAsync()
    {
        return await _context.Players
            .Include(p => p.PlayerRoles)
                .ThenInclude(pr => pr.Role)
            .ToListAsync();
    }

    public async Task AddAsync(Player player)
    {
        await _context.Players.AddAsync(player);
    }

    public void Update(Player player)
    {
        _context.Players.Update(player);
    }

    public void Delete(Player player)
    {
        _context.Players.Remove(player);
    }
}
