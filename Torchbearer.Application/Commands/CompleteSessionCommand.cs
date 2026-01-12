using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record CompleteSessionCommand(
    int SessionId,
    decimal GoldReward,
    int ExperienceReward,
    int RequestingPlayerId) : IRequest<SessionDto>;
