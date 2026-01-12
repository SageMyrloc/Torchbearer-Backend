using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Commands;

public class CompleteSessionCommandHandler : IRequestHandler<CompleteSessionCommand, SessionDto>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionCharacterRepository _sessionCharacterRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteSessionCommandHandler(
        ISessionRepository sessionRepository,
        ISessionCharacterRepository sessionCharacterRepository,
        ICharacterRepository characterRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _sessionCharacterRepository = sessionCharacterRepository;
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SessionDto> Handle(CompleteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetByIdAsync(request.SessionId)
            ?? throw new InvalidOperationException($"Session with id {request.SessionId} not found");

        if (session.GameMasterId != request.RequestingPlayerId)
        {
            throw new UnauthorizedAccessException("Only the Game Master can complete this session");
        }

        session.Complete(request.GoldReward, request.ExperienceReward);
        _sessionRepository.Update(session);

        var sessionCharacters = await _sessionCharacterRepository.GetBySessionIdAsync(request.SessionId);
        foreach (var sc in sessionCharacters)
        {
            var character = await _characterRepository.GetByIdAsync(sc.CharacterId);
            if (character != null)
            {
                character.UpdateGold(character.Gold + request.GoldReward);
                character.UpdateExperiencePoints(character.ExperiencePoints + request.ExperienceReward);
                _characterRepository.Update(character);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var currentPartySize = await _sessionCharacterRepository.CountBySessionIdAsync(session.Id);

        return new SessionDto(
            session.Id,
            session.Title,
            session.Description,
            session.ScheduledAt,
            session.GameMasterId,
            session.GameMaster.Username,
            session.MaxPartySize,
            currentPartySize,
            session.Status.ToString(),
            session.GoldReward,
            session.ExperienceReward,
            session.CreatedAt,
            session.CompletedAt);
    }
}
