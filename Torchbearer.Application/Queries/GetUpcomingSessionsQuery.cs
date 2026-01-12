using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetUpcomingSessionsQuery : IRequest<IEnumerable<SessionDto>>;
