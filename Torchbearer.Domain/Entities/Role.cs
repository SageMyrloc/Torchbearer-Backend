using System.Collections.ObjectModel;

namespace Torchbearer.Domain.Entities;

public class Role
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    private readonly List<PlayerRole> _playerRoles = new();
    public IReadOnlyCollection<PlayerRole> PlayerRoles => new ReadOnlyCollection<PlayerRole>(_playerRoles);

    private Role()
    {
        Name = string.Empty;
    }

    public Role(string name)
    {
        Name = name;
    }
}
