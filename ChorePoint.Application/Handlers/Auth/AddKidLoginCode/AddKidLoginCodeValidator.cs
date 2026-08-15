using FluentValidation;

namespace ChorePoint.Application.Handlers.Auth.AddKidLoginCode;

public class AddKidLoginCodeValidator : AbstractValidator<AddKidLoginCodeCommand>
{
    public AddKidLoginCodeValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
