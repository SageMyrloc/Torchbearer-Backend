using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Application.Commands;

public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, CharacterDto>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCharacterCommandHandler(
        ICharacterRepository characterRepository,
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CharacterDto> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.PlayerId)
            ?? throw new InvalidOperationException($"Player with id {request.PlayerId} not found");

        var characterCount = await _characterRepository.CountByPlayerIdAsync(request.PlayerId);
        if (characterCount >= player.MaxCharacterSlots)
        {
            throw new InvalidOperationException($"Character slot limit reached. Maximum allowed: {player.MaxCharacterSlots}");
        }

        var character = new Character(request.PlayerId, request.Name);

        await _characterRepository.AddAsync(character);
        player.AddCharacter(character);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CharacterDto(
            character.Id,
            character.PlayerId,
            player.Username,
            character.Name,
            character.ImageFileName,
            character.Status.ToString(),
            character.Gold,
            character.ExperiencePoints,
            character.CreatedAt,
            character.ApprovedAt);
    }
}
