using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record CreateCharacterCommand(int PlayerId, string Name) : IRequest<CharacterDto>;
