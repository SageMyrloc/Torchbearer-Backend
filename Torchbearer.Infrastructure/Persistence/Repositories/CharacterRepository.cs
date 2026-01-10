using Microsoft.EntityFrameworkCore;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly TorchbearerDbContext _context;

    public CharacterRepository(TorchbearerDbContext context)
    {
        _context = context;
    }

    public async Task<Character?> GetByIdAsync(int id)
    {
        return await _context.Characters
            .Include(c => c.HexMap)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Character>> GetByPlayerIdAsync(int playerId)
    {
        return await _context.Characters
            .Where(c => c.PlayerId == playerId)
            .ToListAsync();
    }

    public async Task AddAsync(Character character)
    {
        await _context.Characters.AddAsync(character);
    }

    public void Update(Character character)
    {
        _context.Characters.Update(character);
    }

    public void Delete(Character character)
    {
        _context.Characters.Remove(character);
    }
}
