using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Commands;

public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, SessionDto>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionCharacterRepository _sessionCharacterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSessionCommandHandler(
        ISessionRepository sessionRepository,
        ISessionCharacterRepository sessionCharacterRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _sessionCharacterRepository = sessionCharacterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SessionDto> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetByIdAsync(request.SessionId)
            ?? throw new InvalidOperationException($"Session with id {request.SessionId} not found");

        if (session.GameMasterId != request.RequestingPlayerId)
        {
            throw new UnauthorizedAccessException("Only the Game Master can update this session");
        }

        if (session.Status != SessionStatus.Planned)
        {
            throw new InvalidOperationException("Only planned sessions can be updated");
        }

        session.Update(request.Title, request.Description, request.ScheduledAt, request.MaxPartySize);
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
