using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record UpdateSessionCommand(
    int SessionId,
    string Title,
    string? Description,
    DateTime ScheduledAt,
    int? MaxPartySize,
    int RequestingPlayerId) : IRequest<SessionDto>;
