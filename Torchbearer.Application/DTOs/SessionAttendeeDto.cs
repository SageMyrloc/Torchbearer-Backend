namespace Torchbearer.Application.DTOs;

public record SessionAttendeeDto(
    int CharacterId,
    string CharacterName,
    string PlayerName,
    DateTime SignedUpAt);
