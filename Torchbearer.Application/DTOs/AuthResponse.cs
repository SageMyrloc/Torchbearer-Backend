namespace Torchbearer.Application.DTOs;

public record AuthResponse(string Token, string Username, int PlayerId, IEnumerable<string> Roles);
