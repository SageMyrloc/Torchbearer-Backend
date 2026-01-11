using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record UpdateCharacterGoldCommand(int CharacterId, decimal Gold) : IRequest<CharacterDto>;
