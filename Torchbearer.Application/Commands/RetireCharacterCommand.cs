using MediatR;

namespace Torchbearer.Application.Commands;

public record RetireCharacterCommand(int CharacterId, int PlayerId) : IRequest<bool>;
