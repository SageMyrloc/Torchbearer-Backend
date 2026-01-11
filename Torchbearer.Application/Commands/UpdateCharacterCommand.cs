using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record UpdateCharacterCommand(int CharacterId, int PlayerId, string Name) : IRequest<CharacterDto>;
