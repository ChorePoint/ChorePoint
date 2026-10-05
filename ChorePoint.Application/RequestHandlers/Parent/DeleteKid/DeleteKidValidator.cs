using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.Parent.DeleteKid;

public class DeleteKidValidator : AbstractValidator<DeleteKidCommand>
{
    public DeleteKidValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
