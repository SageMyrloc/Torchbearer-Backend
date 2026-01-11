using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Queries;

public class GetAllCharactersQueryHandler : IRequestHandler<GetAllCharactersQuery, IEnumerable<CharacterDto>>
{
    private readonly ICharacterRepository _characterRepository;

    public GetAllCharactersQueryHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<IEnumerable<CharacterDto>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
    {
        var characters = await _characterRepository.GetAllAsync();

        return characters.Select(c => new CharacterDto(
            c.Id,
            c.PlayerId,
            c.Player.Username,
            c.Name,
            c.ImageFileName,
            c.Status.ToString(),
            c.Gold,
            c.ExperiencePoints,
            c.CreatedAt,
            c.ApprovedAt));
    }
}
