using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;

public class CompleteChoreValidator : AbstractValidator<CompleteChoreCommand>
{
    public CompleteChoreValidator()
    {
        RuleFor(x => x.ChoreId)
            .NotEmpty().WithMessage("ChoreId is required");
    }
}