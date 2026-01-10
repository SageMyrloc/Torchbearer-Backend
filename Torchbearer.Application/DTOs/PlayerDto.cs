namespace Torchbearer.Application.DTOs;

public record PlayerDto(int Id, string Username, IEnumerable<string> Roles);
