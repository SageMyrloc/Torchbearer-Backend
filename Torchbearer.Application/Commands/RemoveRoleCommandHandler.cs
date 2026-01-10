using MediatR;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Commands;

public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, bool>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveRoleCommandHandler(
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.PlayerId)
            ?? throw new InvalidOperationException($"Player with id {request.PlayerId} not found");

        var playerRole = player.PlayerRoles.FirstOrDefault(pr => pr.RoleId == request.RoleId);

        if (playerRole == null)
        {
            return true;
        }

        player.RemoveRole(playerRole);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
