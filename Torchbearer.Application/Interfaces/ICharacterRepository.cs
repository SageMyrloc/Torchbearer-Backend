using Torchbearer.Domain.Entities;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Interfaces;

public interface ICharacterRepository
{
    Task<Character?> GetByIdAsync(int id);
    Task<IEnumerable<Character>> GetByPlayerIdAsync(int playerId);
    Task<IEnumerable<Character>> GetAllAsync();
    Task<IEnumerable<Character>> GetByStatusAsync(CharacterStatus status);
    Task<int> CountByPlayerIdAsync(int playerId);
    Task AddAsync(Character character);
    void Update(Character character);
    void Delete(Character character);
}
