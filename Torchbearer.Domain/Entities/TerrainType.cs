using System.Collections.ObjectModel;

namespace Torchbearer.Domain.Entities;

public class TerrainType
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    private readonly List<Hex> _hexes = new();
    public IReadOnlyCollection<Hex> Hexes => new ReadOnlyCollection<Hex>(_hexes);

    private TerrainType()
    {
        Name = string.Empty;
    }

    public TerrainType(string name)
    {
        Name = name;
    }
}
