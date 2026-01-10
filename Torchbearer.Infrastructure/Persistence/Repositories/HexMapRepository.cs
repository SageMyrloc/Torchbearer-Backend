using Microsoft.EntityFrameworkCore;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Infrastructure.Persistence.Repositories;

public class HexMapRepository : IHexMapRepository
{
    private readonly TorchbearerDbContext _context;

    public HexMapRepository(TorchbearerDbContext context)
    {
        _context = context;
    }

    public async Task<HexMap?> GetByIdAsync(int id)
    {
        return await _context.HexMaps
            .Include(h => h.Hexes)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<HexMap?> GetByCharacterIdAsync(int characterId)
    {
        return await _context.HexMaps
            .Include(h => h.Hexes)
            .FirstOrDefaultAsync(h => h.CharacterId == characterId);
    }

    public async Task AddAsync(HexMap hexMap)
    {
        await _context.HexMaps.AddAsync(hexMap);
    }

    public void Update(HexMap hexMap)
    {
        _context.HexMaps.Update(hexMap);
    }
}
