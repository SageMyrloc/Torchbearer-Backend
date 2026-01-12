using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record CreateSessionCommand(
    string Title,
    string? Description,
    DateTime ScheduledAt,
    int? MaxPartySize,
    int GameMasterId) : IRequest<SessionDto>;
