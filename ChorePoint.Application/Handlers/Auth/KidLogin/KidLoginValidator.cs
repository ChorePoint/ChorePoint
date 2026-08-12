using ChorePoint.Application.Handlers.Auth.ParentLogin;

using FluentValidation;

namespace ChorePoint.Application.Handlers.Auth.KidLogin;

public class KidLoginValidator : AbstractValidator<KidLoginCommand>
{
    public KidLoginValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.LoginCode).NotEmpty().WithMessage("LoginCode is required");
    }
}
