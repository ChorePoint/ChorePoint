using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.Auth.AddKidLoginCode;

public class AddKidLoginCodeValidator : AbstractValidator<AddKidLoginCodeCommand>
{
    public AddKidLoginCodeValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
