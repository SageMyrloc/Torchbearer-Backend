using Microsoft.EntityFrameworkCore;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Infrastructure.Persistence.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly TorchbearerDbContext _context;

    public SessionRepository(TorchbearerDbContext context)
    {
        _context = context;
    }

    public async Task<Session?> GetByIdAsync(int id)
    {
        return await _context.Sessions
            .Include(s => s.GameMaster)
            .Include(s => s.SessionCharacters)
                .ThenInclude(sc => sc.Character)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Session>> GetAllAsync()
    {
        return await _context.Sessions
            .Include(s => s.GameMaster)
            .OrderByDescending(s => s.ScheduledAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetByStatusAsync(SessionStatus status)
    {
        return await _context.Sessions
            .Include(s => s.GameMaster)
            .Where(s => s.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetUpcomingAsync()
    {
        return await _context.Sessions
            .Include(s => s.GameMaster)
            .Where(s => s.Status == SessionStatus.Planned)
            .OrderBy(s => s.ScheduledAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetByGameMasterIdAsync(int gameMasterId)
    {
        return await _context.Sessions
            .Include(s => s.GameMaster)
            .Where(s => s.GameMasterId == gameMasterId)
            .ToListAsync();
    }

    public async Task AddAsync(Session session)
    {
        await _context.Sessions.AddAsync(session);
    }

    public void Update(Session session)
    {
        _context.Sessions.Update(session);
    }

    public void Delete(Session session)
    {
        _context.Sessions.Remove(session);
    }
}
