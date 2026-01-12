using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record StartSessionCommand(int SessionId, int RequestingPlayerId) : IRequest<SessionDto>;
