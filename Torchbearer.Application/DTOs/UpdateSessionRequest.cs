namespace Torchbearer.Application.DTOs;

public record UpdateSessionRequest(
    string Title,
    string? Description,
    DateTime ScheduledAt,
    int? MaxPartySize);
