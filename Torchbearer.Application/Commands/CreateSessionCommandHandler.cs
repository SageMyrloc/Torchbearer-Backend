using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Application.Commands;

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, SessionDto>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSessionCommandHandler(
        ISessionRepository sessionRepository,
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SessionDto> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var gameMaster = await _playerRepository.GetByIdAsync(request.GameMasterId)
            ?? throw new InvalidOperationException($"Player with id {request.GameMasterId} not found");

        var session = new Session(
            request.Title,
            request.Description,
            request.ScheduledAt,
            request.GameMasterId,
            request.MaxPartySize);

        await _sessionRepository.AddAsync(session);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SessionDto(
            session.Id,
            session.Title,
            session.Description,
            session.ScheduledAt,
            session.GameMasterId,
            gameMaster.Username,
            session.MaxPartySize,
            0,
            session.Status.ToString(),
            session.GoldReward,
            session.ExperienceReward,
            session.CreatedAt,
            session.CompletedAt);
    }
}
