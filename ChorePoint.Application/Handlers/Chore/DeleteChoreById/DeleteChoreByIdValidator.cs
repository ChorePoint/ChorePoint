using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.DeleteChoreById;

public class DeleteChoreByIdValidator : AbstractValidator<DeleteChoreByIdCommand>
{
    public DeleteChoreByIdValidator()
    {
        RuleFor(x => x.ChoreId)
            .NotEmpty().WithMessage("ChoreId is required");
    }
}