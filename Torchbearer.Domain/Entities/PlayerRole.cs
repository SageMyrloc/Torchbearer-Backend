namespace Torchbearer.Domain.Entities;

public class PlayerRole
{
    public int Id { get; private set; }
    public int PlayerId { get; private set; }
    public int RoleId { get; private set; }

    public Player Player { get; private set; } = null!;
    public Role Role { get; private set; } = null!;

    private PlayerRole()
    {
    }

    public PlayerRole(int playerId, int roleId)
    {
        PlayerId = playerId;
        RoleId = roleId;
    }
}
