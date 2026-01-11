using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Commands;

public class KillCharacterCommandHandler : IRequestHandler<KillCharacterCommand, CharacterDto>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public KillCharacterCommandHandler(
        ICharacterRepository characterRepository,
        IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CharacterDto> Handle(KillCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetByIdAsync(request.CharacterId)
            ?? throw new InvalidOperationException($"Character with id {request.CharacterId} not found");

        if (character.Status != CharacterStatus.Active)
        {
            throw new InvalidOperationException("Only active characters can be killed");
        }

        character.Kill();
        _characterRepository.Update(character);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CharacterDto(
            character.Id,
            character.PlayerId,
            character.Player.Username,
            character.Name,
            character.ImageFileName,
            character.Status.ToString(),
            character.Gold,
            character.ExperiencePoints,
            character.CreatedAt,
            character.ApprovedAt);
    }
}
