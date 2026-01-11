using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Queries;

public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, IEnumerable<PlayerDto>>
{
    private readonly IPlayerRepository _playerRepository;

    public GetAllPlayersQueryHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<IEnumerable<PlayerDto>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
    {
        var players = await _playerRepository.GetAllAsync();

        return players.Select(p => new PlayerDto(
            p.Id,
            p.Username,
            p.MaxCharacterSlots,
            p.PlayerRoles.Select(pr => pr.Role.Name)));
    }
}
