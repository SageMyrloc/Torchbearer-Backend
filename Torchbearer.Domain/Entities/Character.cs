namespace Torchbearer.Domain.Entities;

public class Character
{
    public int Id { get; private set; }
    public int PlayerId { get; private set; }
    public string Name { get; private set; }

    public Player Player { get; private set; } = null!;
    public HexMap? HexMap { get; private set; }

    private Character()
    {
        Name = string.Empty;
    }

    public Character(int playerId, string name)
    {
        PlayerId = playerId;
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void AssignHexMap(HexMap hexMap)
    {
        HexMap = hexMap;
    }
}
