namespace Torchbearer.Domain.Entities;

public class SessionCharacter
{
    public int Id { get; private set; }
    public int SessionId { get; private set; }
    public int CharacterId { get; private set; }
    public DateTime SignedUpAt { get; private set; }

    public Session Session { get; private set; } = null!;
    public Character Character { get; private set; } = null!;

    private SessionCharacter()
    {
    }

    public SessionCharacter(int sessionId, int characterId)
    {
        SessionId = sessionId;
        CharacterId = characterId;
        SignedUpAt = DateTime.UtcNow;
    }
}
