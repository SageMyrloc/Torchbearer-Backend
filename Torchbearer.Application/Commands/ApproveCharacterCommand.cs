using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record ApproveCharacterCommand(int CharacterId) : IRequest<CharacterDto>;
