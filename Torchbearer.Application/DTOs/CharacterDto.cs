namespace Torchbearer.Application.DTOs;

public record CharacterDto(
    int Id,
    int PlayerId,
    string PlayerUsername,
    string Name,
    string? ImageFileName,
    string Status,
    decimal Gold,
    int ExperiencePoints,
    DateTime CreatedAt,
    DateTime? ApprovedAt);
