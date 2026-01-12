using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetSessionByIdQuery(int SessionId) : IRequest<SessionDetailDto?>;
