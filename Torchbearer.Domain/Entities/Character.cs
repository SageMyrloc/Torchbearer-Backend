using Torchbearer.Domain.Enums;

namespace Torchbearer.Domain.Entities;

public class Character
{
    public int Id { get; private set; }
    public int PlayerId { get; private set; }
    public string Name { get; private set; }
    public string? ImageFileName { get; private set; }
    public CharacterStatus Status { get; private set; } = CharacterStatus.PendingApproval;
    public decimal Gold { get; private set; } = 0;
    public int ExperiencePoints { get; private set; } = 0;
    public DateTime CreatedAt { get; private set; }
    public DateTime? ApprovedAt { get; private set; }

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
        CreatedAt = DateTime.UtcNow;
        Status = CharacterStatus.PendingApproval;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void AssignHexMap(HexMap hexMap)
    {
        HexMap = hexMap;
    }

    public void UpdateImageFileName(string imageFileName)
    {
        ImageFileName = imageFileName;
    }

    public void Approve()
    {
        Status = CharacterStatus.Active;
        ApprovedAt = DateTime.UtcNow;
    }

    public void Retire()
    {
        Status = CharacterStatus.Retired;
    }

    public void Kill()
    {
        Status = CharacterStatus.Dead;
    }

    public void Activate()
    {
        Status = CharacterStatus.Active;
    }

    public void UpdateGold(decimal gold)
    {
        Gold = gold;
    }

    public void UpdateExperiencePoints(int experiencePoints)
    {
        ExperiencePoints = experiencePoints;
    }
}
