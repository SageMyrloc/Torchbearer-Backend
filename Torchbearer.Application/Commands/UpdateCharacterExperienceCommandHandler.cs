using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Commands;

public class UpdateCharacterExperienceCommandHandler : IRequestHandler<UpdateCharacterExperienceCommand, CharacterDto>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCharacterExperienceCommandHandler(
        ICharacterRepository characterRepository,
        IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CharacterDto> Handle(UpdateCharacterExperienceCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetByIdAsync(request.CharacterId)
            ?? throw new InvalidOperationException($"Character with id {request.CharacterId} not found");

        character.UpdateExperiencePoints(request.ExperiencePoints);
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
