using System.Collections.ObjectModel;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Domain.Entities;

public class Session
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public int GameMasterId { get; private set; }
    public int? MaxPartySize { get; private set; }
    public SessionStatus Status { get; private set; } = SessionStatus.Planned;
    public decimal GoldReward { get; private set; } = 0;
    public int ExperienceReward { get; private set; } = 0;
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public Player GameMaster { get; private set; } = null!;

    private readonly List<SessionCharacter> _sessionCharacters = new();
    public IReadOnlyCollection<SessionCharacter> SessionCharacters => new ReadOnlyCollection<SessionCharacter>(_sessionCharacters);

    // Future: HexId and PointOfInterestId will be added for expedition linking

    private Session()
    {
        Title = string.Empty;
    }

    public Session(string title, string? description, DateTime scheduledAt, int gameMasterId, int? maxPartySize)
    {
        Title = title;
        Description = description;
        ScheduledAt = scheduledAt;
        GameMasterId = gameMasterId;
        MaxPartySize = maxPartySize;
        Status = SessionStatus.Planned;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string title, string? description, DateTime scheduledAt, int? maxPartySize)
    {
        Title = title;
        Description = description;
        ScheduledAt = scheduledAt;
        MaxPartySize = maxPartySize;
    }

    public void Start()
    {
        if (Status != SessionStatus.Planned)
        {
            throw new InvalidOperationException("Only planned sessions can be started.");
        }

        Status = SessionStatus.InProgress;
    }

    public void Complete(decimal gold, int xp)
    {
        if (Status != SessionStatus.InProgress)
        {
            throw new InvalidOperationException("Only in-progress sessions can be completed.");
        }

        Status = SessionStatus.Completed;
        GoldReward = gold;
        ExperienceReward = xp;
        CompletedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == SessionStatus.Completed)
        {
            throw new InvalidOperationException("Completed sessions cannot be cancelled.");
        }

        Status = SessionStatus.Cancelled;
    }
}
