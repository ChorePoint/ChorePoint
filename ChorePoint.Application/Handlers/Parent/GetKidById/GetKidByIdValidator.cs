using FluentValidation;

namespace ChorePoint.Application.Handlers.Parent.GetKidById;

public class GetKidByIdValidator : AbstractValidator<GetKidByIdQuery>
{
    public GetKidByIdValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
