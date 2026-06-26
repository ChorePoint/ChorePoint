using FluentValidation;

namespace ChorePoint.Application.Handlers.Parent.DeleteKidById;

public class DeleteKidByIdValidator : AbstractValidator<DeleteKidByIdCommand>
{
    public DeleteKidByIdValidator()
    {
        RuleFor(x => x.KidId)
            .NotEmpty().WithMessage("KidId is required");
    }
}