using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetCharacterByIdQuery(int CharacterId, int PlayerId) : IRequest<CharacterDto>;
