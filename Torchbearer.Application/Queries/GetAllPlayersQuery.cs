using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetAllPlayersQuery : IRequest<IEnumerable<PlayerDto>>;
