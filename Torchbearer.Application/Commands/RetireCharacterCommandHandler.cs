using MediatR;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Commands;

public class RetireCharacterCommandHandler : IRequestHandler<RetireCharacterCommand, bool>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RetireCharacterCommandHandler(
        ICharacterRepository characterRepository,
        IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RetireCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetByIdAsync(request.CharacterId)
            ?? throw new InvalidOperationException($"Character with id {request.CharacterId} not found");

        if (character.PlayerId != request.PlayerId)
        {
            throw new UnauthorizedAccessException("You do not own this character");
        }

        if (character.Status != CharacterStatus.Active)
        {
            throw new InvalidOperationException("Only active characters can be retired");
        }

        character.Retire();
        _characterRepository.Update(character);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
