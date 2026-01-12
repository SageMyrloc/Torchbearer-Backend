using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;

namespace Torchbearer.Application.Queries;

public class GetMySessionsQueryHandler : IRequestHandler<GetMySessionsQuery, IEnumerable<SessionDto>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly ISessionCharacterRepository _sessionCharacterRepository;

    public GetMySessionsQueryHandler(
        ISessionRepository sessionRepository,
        ICharacterRepository characterRepository,
        ISessionCharacterRepository sessionCharacterRepository)
    {
        _sessionRepository = sessionRepository;
        _characterRepository = characterRepository;
        _sessionCharacterRepository = sessionCharacterRepository;
    }

    public async Task<IEnumerable<SessionDto>> Handle(GetMySessionsQuery request, CancellationToken cancellationToken)
    {
        var playerCharacters = await _characterRepository.GetByPlayerIdAsync(request.PlayerId);
        var characterIds = playerCharacters.Select(c => c.Id).ToHashSet();

        var allSessions = await _sessionRepository.GetAllAsync();

        var sessionDtos = new List<SessionDto>();
        foreach (var session in allSessions)
        {
            var sessionCharacters = await _sessionCharacterRepository.GetBySessionIdAsync(session.Id);
            var isSignedUp = sessionCharacters.Any(sc => characterIds.Contains(sc.CharacterId));

            if (isSignedUp)
            {
                sessionDtos.Add(new SessionDto(
                    session.Id,
                    session.Title,
                    session.Description,
                    session.ScheduledAt,
                    session.GameMasterId,
                    session.GameMaster.Username,
                    session.MaxPartySize,
                    sessionCharacters.Count(),
                    session.Status.ToString(),
                    session.GoldReward,
                    session.ExperienceReward,
                    session.CreatedAt,
                    session.CompletedAt));
            }
        }

        return sessionDtos;
    }
}
