using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record UploadCharacterImageCommand(int CharacterId, int PlayerId, string FileName) : IRequest<CharacterDto>;
