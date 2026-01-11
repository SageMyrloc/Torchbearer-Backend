using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record KillCharacterCommand(int CharacterId) : IRequest<CharacterDto>;
