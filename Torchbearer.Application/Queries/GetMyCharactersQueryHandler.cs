using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Queries;

public class GetMyCharactersQueryHandler : IRequestHandler<GetMyCharactersQuery, MyCharactersResponseDto>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IPlayerRepository _playerRepository;

    public GetMyCharactersQueryHandler(ICharacterRepository characterRepository, IPlayerRepository playerRepository)
    {
        _characterRepository = characterRepository;
        _playerRepository = playerRepository;
    }

    public async Task<MyCharactersResponseDto> Handle(GetMyCharactersQuery request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.PlayerId);
        var characters = await _characterRepository.GetByPlayerIdAsync(request.PlayerId);

        var characterDtos = characters.Select(c => new CharacterSummaryDto(
            c.Id,
            c.Name,
            c.Status.ToString(),
            c.ImageFileName)).ToList();

        return new MyCharactersResponseDto(
            characterDtos,
            characterDtos.Count,
            player?.MaxCharacterSlots ?? 0);
    }
}
