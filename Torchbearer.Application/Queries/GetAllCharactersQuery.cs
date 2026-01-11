using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetAllCharactersQuery : IRequest<IEnumerable<CharacterDto>>;
