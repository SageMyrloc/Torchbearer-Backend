using MediatR;

namespace Torchbearer.Application.Commands;

public record RemoveAttendeeCommand(int SessionId, int CharacterId, int RequestingPlayerId) : IRequest<bool>;
