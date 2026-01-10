using System.Collections.ObjectModel;

namespace Torchbearer.Domain.Entities;

public class Player
{
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }

    private readonly List<PlayerRole> _playerRoles = new();
    public IReadOnlyCollection<PlayerRole> PlayerRoles => new ReadOnlyCollection<PlayerRole>(_playerRoles);

    private readonly List<Character> _characters = new();
    public IReadOnlyCollection<Character> Characters => new ReadOnlyCollection<Character>(_characters);

    private Player()
    {
        Username = string.Empty;
        PasswordHash = string.Empty;
    }

    public Player(string username, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void AddRole(PlayerRole role)
    {
        if (!_playerRoles.Contains(role))
        {
            _playerRoles.Add(role);
        }
    }

    public void RemoveRole(PlayerRole role)
    {
        _playerRoles.Remove(role);
    }

    public void AddCharacter(Character character)
    {
        if (!_characters.Contains(character))
        {
            _characters.Add(character);
        }
    }
}
