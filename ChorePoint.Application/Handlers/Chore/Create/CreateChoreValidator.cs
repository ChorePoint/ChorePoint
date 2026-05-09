using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChorePoint.Application.Handlers.Chore.Create
{
    public class CreateChoreValidator : AbstractValidator<CreateChoreCommand>
    {
        public CreateChoreValidator() { }
    }
}
