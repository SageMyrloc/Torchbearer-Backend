using MediatR;

namespace Torchbearer.Application.Commands;

public record AssignRoleCommand(int PlayerId, int RoleId) : IRequest<bool>;
