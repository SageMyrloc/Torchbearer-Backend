using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Queries;

public class GetUpcomingSessionsQueryHandler : IRequestHandler<GetUpcomingSessionsQuery, IEnumerable<SessionDto>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionCharacterRepository _sessionCharacterRepository;

    public GetUpcomingSessionsQueryHandler(
        ISessionRepository sessionRepository,
        ISessionCharacterRepository sessionCharacterRepository)
    {
        _sessionRepository = sessionRepository;
        _sessionCharacterRepository = sessionCharacterRepository;
    }

    public async Task<IEnumerable<SessionDto>> Handle(GetUpcomingSessionsQuery request, CancellationToken cancellationToken)
    {
        var sessions = await _sessionRepository.GetUpcomingAsync();

        var sessionDtos = new List<SessionDto>();
        foreach (var session in sessions)
        {
            var currentPartySize = await _sessionCharacterRepository.CountBySessionIdAsync(session.Id);
            sessionDtos.Add(new SessionDto(
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
                session.CompletedAt));
        }

        return sessionDtos;
    }
}
