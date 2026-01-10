using MediatR;

namespace Torchbearer.Application.Commands;

public record RemoveRoleCommand(int PlayerId, int RoleId) : IRequest<bool>;
