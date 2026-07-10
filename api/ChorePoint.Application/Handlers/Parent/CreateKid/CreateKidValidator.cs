using FluentValidation;

namespace ChorePoint.Application.Handlers.Parent.CreateKid;

public class CreateKidValidator : AbstractValidator<CreateKidCommand>
{
    public CreateKidValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Avatar)
            .NotEmpty().WithMessage("Avatar is required");
    }
}