using System.Collections.ObjectModel;

namespace Torchbearer.Domain.Entities;

public class PointOfInterest
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private readonly List<PointOfInterestHex> _pointOfInterestHexes = new();
    public IReadOnlyCollection<PointOfInterestHex> PointOfInterestHexes => new ReadOnlyCollection<PointOfInterestHex>(_pointOfInterestHexes);

    private PointOfInterest()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public PointOfInterest(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void UpdateDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
