using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record ActivateCharacterCommand(int CharacterId) : IRequest<CharacterDto>;
