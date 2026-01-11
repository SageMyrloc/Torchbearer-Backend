using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Queries;

public class GetMyCharactersQueryHandler : IRequestHandler<GetMyCharactersQuery, IEnumerable<CharacterSummaryDto>>
{
    private readonly ICharacterRepository _characterRepository;

    public GetMyCharactersQueryHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<IEnumerable<CharacterSummaryDto>> Handle(GetMyCharactersQuery request, CancellationToken cancellationToken)
    {
        var characters = await _characterRepository.GetByPlayerIdAsync(request.PlayerId);

        return characters.Select(c => new CharacterSummaryDto(
            c.Id,
            c.Name,
            c.Status.ToString(),
            c.ImageFileName));
    }
}
