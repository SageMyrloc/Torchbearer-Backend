using System.Collections.ObjectModel;

namespace Torchbearer.Domain.Entities;

public class HexMap
{
    public int Id { get; private set; }
    public int CharacterId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Character Character { get; private set; } = null!;

    private readonly List<Hex> _hexes = new();
    public IReadOnlyCollection<Hex> Hexes => new ReadOnlyCollection<Hex>(_hexes);

    private HexMap()
    {
    }

    public HexMap(int characterId)
    {
        CharacterId = characterId;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddHex(Hex hex)
    {
        if (!_hexes.Contains(hex))
        {
            _hexes.Add(hex);
        }
    }

    public void RemoveHex(Hex hex)
    {
        _hexes.Remove(hex);
    }
}
