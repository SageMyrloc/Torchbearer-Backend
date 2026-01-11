namespace Torchbearer.Application.DTOs;

public record PlayerDto(int Id, string Username, int MaxCharacterSlots, IEnumerable<string> Roles);
