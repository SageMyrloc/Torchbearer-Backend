using MediatR;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Commands;

public class SignUpForSessionCommandHandler : IRequestHandler<SignUpForSessionCommand, bool>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly ISessionCharacterRepository _sessionCharacterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SignUpForSessionCommandHandler(
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

    public async Task<bool> Handle(SignUpForSessionCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetByIdAsync(request.CharacterId)
            ?? throw new InvalidOperationException($"Character with id {request.CharacterId} not found");

        if (character.PlayerId != request.PlayerId)
        {
            throw new UnauthorizedAccessException("You do not own this character");
        }

        if (character.Status != CharacterStatus.Active)
        {
            throw new InvalidOperationException("Only active characters can sign up for sessions");
        }

        var session = await _sessionRepository.GetByIdAsync(request.SessionId)
            ?? throw new InvalidOperationException($"Session with id {request.SessionId} not found");

        if (session.Status != SessionStatus.Planned)
        {
            throw new InvalidOperationException("Can only sign up for planned sessions");
        }

        var existingSignUp = await _sessionCharacterRepository.GetBySessionAndCharacterAsync(request.SessionId, request.CharacterId);
        if (existingSignUp != null)
        {
            throw new InvalidOperationException("Character is already signed up for this session");
        }

        if (session.MaxPartySize.HasValue)
        {
            var currentPartySize = await _sessionCharacterRepository.CountBySessionIdAsync(request.SessionId);
            if (currentPartySize >= session.MaxPartySize.Value)
            {
                throw new InvalidOperationException("Session is full");
            }
        }

        var sessionCharacter = new SessionCharacter(request.SessionId, request.CharacterId);
        await _sessionCharacterRepository.AddAsync(sessionCharacter);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
