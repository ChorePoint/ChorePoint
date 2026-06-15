using FluentValidation;

namespace ChorePoint.Application.Handlers.Chore.CreateChore;

public class CreateChoreValidator : AbstractValidator<CreateChoreCommand>
{
    public CreateChoreValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Icon)
            .NotEmpty().WithMessage("Icon is required");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Points)
            .NotEmpty().WithMessage("Points is required");

        RuleFor(x => x.Difficulty)
            .NotEmpty().WithMessage("Difficulty is required");

        RuleFor(x => x.Frequency)
            .NotEmpty().WithMessage("Frequency is required");

        RuleFor(x => x.KidIds)
            .NotEmpty().WithMessage("KidIds is required");

        RuleFor(x => x.Visibilities)
            .NotEmpty().WithMessage("Visibilities is required");
    }
}