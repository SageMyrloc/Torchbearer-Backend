using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record CancelSessionCommand(int SessionId, int RequestingPlayerId) : IRequest<SessionDto>;
