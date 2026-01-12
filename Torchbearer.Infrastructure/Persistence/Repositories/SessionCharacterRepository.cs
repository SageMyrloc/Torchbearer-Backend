using Microsoft.EntityFrameworkCore;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Repositories;

public class SessionCharacterRepository : ISessionCharacterRepository
{
    private readonly TorchbearerDbContext _context;

    public SessionCharacterRepository(TorchbearerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SessionCharacter>> GetBySessionIdAsync(int sessionId)
    {
        return await _context.SessionCharacters
            .Include(sc => sc.Character)
            .Where(sc => sc.SessionId == sessionId)
            .ToListAsync();
    }

    public async Task<IEnumerable<SessionCharacter>> GetByCharacterIdAsync(int characterId)
    {
        return await _context.SessionCharacters
            .Include(sc => sc.Session)
            .Where(sc => sc.CharacterId == characterId)
            .ToListAsync();
    }

    public async Task<SessionCharacter?> GetBySessionAndCharacterAsync(int sessionId, int characterId)
    {
        return await _context.SessionCharacters
            .FirstOrDefaultAsync(sc => sc.SessionId == sessionId && sc.CharacterId == characterId);
    }

    public async Task<int> CountBySessionIdAsync(int sessionId)
    {
        return await _context.SessionCharacters
            .CountAsync(sc => sc.SessionId == sessionId);
    }

    public async Task AddAsync(SessionCharacter sessionCharacter)
    {
        await _context.SessionCharacters.AddAsync(sessionCharacter);
    }

    public void Delete(SessionCharacter sessionCharacter)
    {
        _context.SessionCharacters.Remove(sessionCharacter);
    }
}
