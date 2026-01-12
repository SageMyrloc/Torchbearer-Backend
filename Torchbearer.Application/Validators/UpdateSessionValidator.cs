using FluentValidation;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Validators;

public class UpdateSessionValidator : AbstractValidator<UpdateSessionRequest>
{
    public UpdateSessionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.ScheduledAt)
            .GreaterThan(DateTime.UtcNow).WithMessage("Scheduled date must be in the future.");

        RuleFor(x => x.MaxPartySize)
            .InclusiveBetween(1, 10).WithMessage("Max party size must be between 1 and 10.")
            .When(x => x.MaxPartySize.HasValue);
    }
}
