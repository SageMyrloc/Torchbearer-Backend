namespace Torchbearer.Application.DTOs;

public record SessionDetailDto(
    int Id,
    string Title,
    string? Description,
    DateTime ScheduledAt,
    int GameMasterId,
    string GameMasterName,
    int? MaxPartySize,
    int CurrentPartySize,
    string Status,
    decimal GoldReward,
    int ExperienceReward,
    DateTime CreatedAt,
    DateTime? CompletedAt,
    IEnumerable<SessionAttendeeDto> Attendees);
