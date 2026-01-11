using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetMyCharactersQuery(int PlayerId) : IRequest<IEnumerable<CharacterSummaryDto>>;
