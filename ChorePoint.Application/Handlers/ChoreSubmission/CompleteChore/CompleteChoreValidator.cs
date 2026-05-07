using FluentValidation;

namespace ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;

public class CompleteChoreValidator : AbstractValidator<CompleteChoreCommand>
{
    public CompleteChoreValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}