using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record UpdateCharacterExperienceCommand(int CharacterId, int ExperiencePoints) : IRequest<CharacterDto>;
