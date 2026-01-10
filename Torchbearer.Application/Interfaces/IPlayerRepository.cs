using Torchbearer.Domain.Entities;

namespace Torchbearer.Application.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetByIdAsync(int id);
    Task<Player?> GetByUsernameAsync(string username);
    Task<IEnumerable<Player>> GetAllAsync();
    Task AddAsync(Player player);
    void Update(Player player);
    void Delete(Player player);
}
