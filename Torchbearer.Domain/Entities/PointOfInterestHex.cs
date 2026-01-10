namespace Torchbearer.Domain.Entities;

public class PointOfInterestHex
{
    public int Id { get; private set; }
    public int HexMapId { get; private set; }
    public int HexId { get; private set; }
    public int PointOfInterestId { get; private set; }

    public HexMap HexMap { get; private set; } = null!;
    public Hex Hex { get; private set; } = null!;
    public PointOfInterest PointOfInterest { get; private set; } = null!;

    private PointOfInterestHex()
    {
    }

    public PointOfInterestHex(int hexMapId, int hexId, int pointOfInterestId)
    {
        HexMapId = hexMapId;
        HexId = hexId;
        PointOfInterestId = pointOfInterestId;
    }
}
