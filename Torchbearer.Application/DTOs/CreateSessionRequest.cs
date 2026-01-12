namespace Torchbearer.Application.DTOs;

public record CreateSessionRequest(
    string Title,
    string? Description,
    DateTime ScheduledAt,
    int? MaxPartySize);
