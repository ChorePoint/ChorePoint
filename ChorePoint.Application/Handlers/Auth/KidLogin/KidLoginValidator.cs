using FluentValidation;

namespace ChorePoint.Application.Handlers.Auth.KidLogin;

public class KidLoginValidator : AbstractValidator<KidLoginCommand>
{
    public KidLoginValidator()
    {
        RuleFor(x => x.LoginCode).NotEmpty().WithMessage("LoginCode is required");
    }
}
