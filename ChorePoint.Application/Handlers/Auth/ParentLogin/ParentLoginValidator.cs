using FluentValidation;

namespace ChorePoint.Application.Handlers.Auth.ParentLogin;

public class ParentLoginValidator : AbstractValidator<ParentLoginCommand>
{
    public ParentLoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email address");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}
