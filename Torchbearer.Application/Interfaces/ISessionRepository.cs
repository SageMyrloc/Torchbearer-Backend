using Torchbearer.Domain.Entities;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Interfaces;

public interface ISessionRepository
{
    Task<Session?> GetByIdAsync(int id);
    Task<IEnumerable<Session>> GetAllAsync();
    Task<IEnumerable<Session>> GetByStatusAsync(SessionStatus status);
    Task<IEnumerable<Session>> GetUpcomingAsync();
    Task<IEnumerable<Session>> GetByGameMasterIdAsync(int gameMasterId);
    Task AddAsync(Session session);
    void Update(Session session);
    void Delete(Session session);
}
