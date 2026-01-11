namespace Torchbearer.Application.DTOs;

public record CharacterSummaryDto(
    int Id,
    string Name,
    string Status,
    string? ImageFileName);
