using MediatR;

namespace Torchbearer.Application.Commands;

public record WithdrawFromSessionCommand(int SessionId, int CharacterId, int PlayerId) : IRequest<bool>;
