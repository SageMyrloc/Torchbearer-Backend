using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Queries;

public class GetSessionByIdQueryHandler : IRequestHandler<GetSessionByIdQuery, SessionDetailDto?>
{
    private readonly ISessionRepository _sessionRepository;

    public GetSessionByIdQueryHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<SessionDetailDto?> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetByIdAsync(request.SessionId);

        if (session == null)
        {
            return null;
        }

        var attendees = session.SessionCharacters.Select(sc => new SessionAttendeeDto(
            sc.CharacterId,
            sc.Character.Name,
            sc.Character.Player.Username,
            sc.SignedUpAt));

        return new SessionDetailDto(
            session.Id,
            session.Title,
            session.Description,
            session.ScheduledAt,
            session.GameMasterId,
            session.GameMaster.Username,
            session.MaxPartySize,
            session.SessionCharacters.Count,
            session.Status.ToString(),
            session.GoldReward,
            session.ExperienceReward,
            session.CreatedAt,
            session.CompletedAt,
            attendees);
    }
}
