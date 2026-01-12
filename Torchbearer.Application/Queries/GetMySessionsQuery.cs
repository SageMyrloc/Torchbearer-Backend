using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetMySessionsQuery(int PlayerId) : IRequest<IEnumerable<SessionDto>>;
