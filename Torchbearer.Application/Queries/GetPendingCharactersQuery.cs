using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetPendingCharactersQuery : IRequest<IEnumerable<CharacterDto>>;
