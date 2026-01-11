using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Commands;

public class UpdatePlayerSlotsCommandHandler : IRequestHandler<UpdatePlayerSlotsCommand, PlayerDto>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePlayerSlotsCommandHandler(
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PlayerDto> Handle(UpdatePlayerSlotsCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.PlayerId)
            ?? throw new InvalidOperationException($"Player with id {request.PlayerId} not found");

        player.UpdateMaxCharacterSlots(request.MaxSlots);
        _playerRepository.Update(player);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new PlayerDto(
            player.Id,
            player.Username,
            player.MaxCharacterSlots,
            player.PlayerRoles.Select(pr => pr.Role.Name));
    }
}
