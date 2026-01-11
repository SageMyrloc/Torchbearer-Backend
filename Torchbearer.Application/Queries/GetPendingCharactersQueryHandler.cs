using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Queries;

public class GetPendingCharactersQueryHandler : IRequestHandler<GetPendingCharactersQuery, IEnumerable<CharacterDto>>
{
    private readonly ICharacterRepository _characterRepository;

    public GetPendingCharactersQueryHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<IEnumerable<CharacterDto>> Handle(GetPendingCharactersQuery request, CancellationToken cancellationToken)
    {
        var characters = await _characterRepository.GetByStatusAsync(CharacterStatus.PendingApproval);

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
