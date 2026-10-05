using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.Parent.GetKidById;

public class GetKidByIdValidator : AbstractValidator<GetKidByIdQuery>
{
    public GetKidByIdValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
