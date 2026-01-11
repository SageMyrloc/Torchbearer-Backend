using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Queries;

public class GetCurrentPlayerQueryHandler : IRequestHandler<GetCurrentPlayerQuery, PlayerDto>
{
    private readonly IPlayerRepository _playerRepository;

    public GetCurrentPlayerQueryHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<PlayerDto> Handle(GetCurrentPlayerQuery request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.PlayerId)
            ?? throw new InvalidOperationException($"Player with id {request.PlayerId} not found");

        return new PlayerDto(
            player.Id,
            player.Username,
            player.MaxCharacterSlots,
            player.PlayerRoles.Select(pr => pr.Role.Name));
    }
}
