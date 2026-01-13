using MediatR;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Enums;

namespace Torchbearer.Application.Commands;

public class RemoveAttendeeCommandHandler : IRequestHandler<RemoveAttendeeCommand, bool>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ISessionCharacterRepository _sessionCharacterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAttendeeCommandHandler(
        ISessionRepository sessionRepository,
        ISessionCharacterRepository sessionCharacterRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _sessionCharacterRepository = sessionCharacterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RemoveAttendeeCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetByIdAsync(request.SessionId)
            ?? throw new InvalidOperationException($"Session with id {request.SessionId} not found");

        if (session.GameMasterId != request.RequestingPlayerId)
        {
            throw new UnauthorizedAccessException("Only the Game Master can remove attendees from a session");
        }

        if (session.Status != SessionStatus.Planned)
        {
            throw new InvalidOperationException("Can only remove attendees from planned sessions");
        }

        var sessionCharacter = await _sessionCharacterRepository.GetBySessionAndCharacterAsync(request.SessionId, request.CharacterId)
            ?? throw new InvalidOperationException("Character is not signed up for this session");

        _sessionCharacterRepository.Delete(sessionCharacter);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
