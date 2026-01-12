using FluentValidation;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Validators;

public class CompleteSessionValidator : AbstractValidator<CompleteSessionRequest>
{
    public CompleteSessionValidator()
    {
        RuleFor(x => x.GoldReward)
            .GreaterThanOrEqualTo(0).WithMessage("Gold reward must be 0 or greater.");

        RuleFor(x => x.ExperienceReward)
            .GreaterThanOrEqualTo(0).WithMessage("Experience reward must be 0 or greater.");
    }
}
