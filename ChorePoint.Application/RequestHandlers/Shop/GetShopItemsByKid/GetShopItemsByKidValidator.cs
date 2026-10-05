using FluentValidation;

namespace ChorePoint.Application.RequestHandlers.Shop.GetShopItemsByKid;

public class GetShopItemsByKidValidator : AbstractValidator<GetShopItemsByKidQuery>
{
    public GetShopItemsByKidValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
