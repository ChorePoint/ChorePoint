using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.DeleteChore;

public class DeleteChoreValidator : AbstractValidator<DeleteChoreCommand>
{
    public DeleteChoreValidator()
    {
        RuleFor(x => x.ChoreId)
            .NotEmpty().WithMessage("ChoreId is required");
    }
}