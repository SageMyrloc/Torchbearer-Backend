using FluentValidation;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Validators;

public class UpdateCharacterValidator : AbstractValidator<UpdateCharacterRequest>
{
    private const string NamePattern = @"^[\p{L}][\p{L}\s\-']*[\p{L}]$|^[\p{L}]{1,2}$";

    public UpdateCharacterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.")
            .Matches(NamePattern).WithMessage("Name must start and end with a letter and can only contain letters, spaces, hyphens, and apostrophes.");
    }
}
