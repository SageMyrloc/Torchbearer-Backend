using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Queries;

public record GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>;
