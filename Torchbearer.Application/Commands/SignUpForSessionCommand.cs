using MediatR;

namespace Torchbearer.Application.Commands;

public record SignUpForSessionCommand(int SessionId, int CharacterId, int PlayerId) : IRequest<bool>;
