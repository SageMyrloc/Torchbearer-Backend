using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record UpdatePlayerSlotsCommand(int PlayerId, int MaxSlots) : IRequest<PlayerDto>;
