using Torchbearer.Domain.Entities;

namespace Torchbearer.Application.Interfaces;

public interface ISessionCharacterRepository
{
    Task<IEnumerable<SessionCharacter>> GetBySessionIdAsync(int sessionId);
    Task<IEnumerable<SessionCharacter>> GetByCharacterIdAsync(int characterId);
    Task<SessionCharacter?> GetBySessionAndCharacterAsync(int sessionId, int characterId);
    Task<int> CountBySessionIdAsync(int sessionId);
    Task AddAsync(SessionCharacter sessionCharacter);
    void Delete(SessionCharacter sessionCharacter);
}
