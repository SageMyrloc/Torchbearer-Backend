using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetDMSessionsQuery(int GameMasterId) : IRequest<IEnumerable<SessionDto>>;
