using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetCurrentPlayerQuery(int PlayerId) : IRequest<PlayerDto>;
