using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Commands;

public class StartSessionCommandHandler : IRequestHandler<StartSessionCommand, SessionDto>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionCharacterRepository _sessionCharacterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StartSessionCommandHandler(
        ISessionRepository sessionRepository,
        ISessionCharacterRepository sessionCharacterRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _sessionCharacterRepository = sessionCharacterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SessionDto> Handle(StartSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetByIdAsync(request.SessionId)
            ?? throw new InvalidOperationException($"Session with id {request.SessionId} not found");

        if (session.GameMasterId != request.RequestingPlayerId)
        {
            throw new UnauthorizedAccessException("Only the Game Master can start this session");
        }

        session.Start();
        _sessionRepository.Update(session);
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
