namespace Torchbearer.Application.DTOs;

public record CompleteSessionRequest(
    decimal GoldReward,
    int ExperienceReward);
