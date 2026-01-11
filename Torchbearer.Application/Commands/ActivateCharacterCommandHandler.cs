using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Commands;

public class ActivateCharacterCommandHandler : IRequestHandler<ActivateCharacterCommand, CharacterDto>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateCharacterCommandHandler(
        ICharacterRepository characterRepository,
        IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CharacterDto> Handle(ActivateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetByIdAsync(request.CharacterId)
            ?? throw new InvalidOperationException($"Character with id {request.CharacterId} not found");

        if (character.Status != CharacterStatus.Retired && character.Status != CharacterStatus.Dead)
        {
            throw new InvalidOperationException("Only retired or dead characters can be activated");
        }

        character.Activate();
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
