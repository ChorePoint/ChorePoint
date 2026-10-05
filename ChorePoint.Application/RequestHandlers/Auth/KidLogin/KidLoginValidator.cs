using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.Auth.KidLogin;

public class KidLoginValidator : AbstractValidator<KidLoginCommand>
{
    public KidLoginValidator()
    {
        RuleFor(x => x.LoginCode).NotEmpty().WithMessage("LoginCode is required");
    }
}
