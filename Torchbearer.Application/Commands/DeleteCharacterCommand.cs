using MediatR;

namespace Torchbearer.Application.Commands;

public record DeleteCharacterCommand(int CharacterId) : IRequest<bool>;
