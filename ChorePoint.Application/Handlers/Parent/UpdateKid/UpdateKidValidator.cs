using ChorePoint.Application.Handlers.Chore.UpdateChore;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChorePoint.Application.Handlers.Parent.UpdateKid;

public class UpdateKidValidator : AbstractValidator<UpdateKidCommand>
{
    public UpdateKidValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Age)
            .NotEmpty().WithMessage("Age is required");

        RuleFor(x => x.Avatar)
            .NotEmpty().WithMessage("Icon is required");

        RuleFor(x => x.SpendablePoints)
            .NotEmpty().WithMessage("SpendablePoints is required");

        RuleFor(x => x.DayStreak)
            .NotEmpty().WithMessage("DayStreak is required");

    }
}
