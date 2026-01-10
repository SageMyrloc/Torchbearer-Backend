using MediatR;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Application.Commands;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleCommandHandler(
        IPlayerRepository playerRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.PlayerId)
            ?? throw new InvalidOperationException($"Player with id {request.PlayerId} not found");

        var role = await _roleRepository.GetByIdAsync(request.RoleId)
            ?? throw new InvalidOperationException($"Role with id {request.RoleId} not found");

        if (player.PlayerRoles.Any(pr => pr.RoleId == role.Id))
        {
            return true;
        }

        var playerRole = new PlayerRole(player.Id, role.Id);
        player.AddRole(playerRole);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
