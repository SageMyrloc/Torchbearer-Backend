using MediatR;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Commands;

public class WithdrawFromSessionCommandHandler : IRequestHandler<WithdrawFromSessionCommand, bool>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly ISessionCharacterRepository _sessionCharacterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WithdrawFromSessionCommandHandler(
        ISessionRepository sessionRepository,
        ICharacterRepository characterRepository,
        ISessionCharacterRepository sessionCharacterRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _characterRepository = characterRepository;
        _sessionCharacterRepository = sessionCharacterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(WithdrawFromSessionCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetByIdAsync(request.CharacterId)
            ?? throw new InvalidOperationException($"Character with id {request.CharacterId} not found");

        if (character.PlayerId != request.PlayerId)
        {
            throw new UnauthorizedAccessException("You do not own this character");
        }

        var session = await _sessionRepository.GetByIdAsync(request.SessionId)
            ?? throw new InvalidOperationException($"Session with id {request.SessionId} not found");

        if (session.Status != SessionStatus.Planned)
        {
            throw new InvalidOperationException("Can only withdraw from planned sessions");
        }

        var sessionCharacter = await _sessionCharacterRepository.GetBySessionAndCharacterAsync(request.SessionId, request.CharacterId)
            ?? throw new InvalidOperationException("Character is not signed up for this session");

        _sessionCharacterRepository.Delete(sessionCharacter);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
