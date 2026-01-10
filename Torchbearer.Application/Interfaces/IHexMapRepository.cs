using Torchbearer.Domain.Entities;

namespace Torchbearer.Application.Interfaces;

public interface IHexMapRepository
{
    Task<HexMap?> GetByIdAsync(int id);
    Task<HexMap?> GetByCharacterIdAsync(int characterId);
    Task AddAsync(HexMap hexMap);
    void Update(HexMap hexMap);
}
