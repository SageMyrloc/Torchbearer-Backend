using System.Collections.ObjectModel;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Domain.Entities;

public class Hex
{
    public int Id { get; private set; }
    public int HexMapId { get; private set; }

    // Visible coordinates
    public int Q { get; private set; }
    public int R { get; private set; }
    public int S { get; private set; }

    // True coordinates
    public int TQ { get; private set; }
    public int TR { get; private set; }
    public int TS { get; private set; }

    public int TerrainTypeId { get; private set; }
    public HexStatus Status { get; private set; }

    public HexMap HexMap { get; private set; } = null!;
    public TerrainType TerrainType { get; private set; } = null!;

    private readonly List<PointOfInterestHex> _pointOfInterestHexes = new();
    public IReadOnlyCollection<PointOfInterestHex> PointOfInterestHexes => new ReadOnlyCollection<PointOfInterestHex>(_pointOfInterestHexes);

    private Hex()
    {
    }

    public Hex(int hexMapId, int q, int r, int s, int tq, int tr, int ts, int terrainTypeId, HexStatus status)
    {
        if (q + r + s != 0)
        {
            throw new ArgumentException("Visible cube coordinates must sum to zero (q + r + s = 0)");
        }

        if (tq + tr + ts != 0)
        {
            throw new ArgumentException("True cube coordinates must sum to zero (tq + tr + ts = 0)");
        }

        HexMapId = hexMapId;
        Q = q;
        R = r;
        S = s;
        TQ = tq;
        TR = tr;
        TS = ts;
        TerrainTypeId = terrainTypeId;
        Status = status;
    }

    public void UpdateStatus(HexStatus status)
    {
        Status = status;
    }

    public void UpdateVisibleCoordinates(int q, int r, int s)
    {
        if (q + r + s != 0)
        {
            throw new ArgumentException("Visible cube coordinates must sum to zero (q + r + s = 0)");
        }

        Q = q;
        R = r;
        S = s;
    }

    public void AddPointOfInterest(PointOfInterestHex pointOfInterestHex)
    {
        if (!_pointOfInterestHexes.Contains(pointOfInterestHex))
        {
            _pointOfInterestHexes.Add(pointOfInterestHex);
        }
    }
}
