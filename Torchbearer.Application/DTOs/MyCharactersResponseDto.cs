namespace Torchbearer.Application.DTOs;

public record MyCharactersResponseDto(
    IEnumerable<CharacterSummaryDto> Characters,
    int CharacterCount,
    int MaxCharacterSlots);
