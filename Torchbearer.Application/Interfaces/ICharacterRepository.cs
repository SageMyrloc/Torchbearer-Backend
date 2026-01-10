using Torchbearer.Domain.Entities;

namespace Torchbearer.Application.Interfaces;

public interface ICharacterRepository
{
    Task<Character?> GetByIdAsync(int id);
    Task<IEnumerable<Character>> GetByPlayerIdAsync(int playerId);
    Task AddAsync(Character character);
    void Update(Character character);
    void Delete(Character character);
}
