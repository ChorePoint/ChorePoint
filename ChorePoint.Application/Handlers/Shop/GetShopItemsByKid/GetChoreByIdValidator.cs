using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

public class GetChoreByIdValidator : AbstractValidator<GetShopItemsByKidQuery>
{
    public GetChoreByIdValidator()
    {
        RuleFor(x => x.KidId)
            .NotEmpty().WithMessage("KidId is required");
    }
}