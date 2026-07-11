using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.GetShopItemsByKid;

public class GetShopItemsByKidValidator : AbstractValidator<GetShopItemsByKidQuery>
{
    public GetShopItemsByKidValidator()
    {
        RuleFor(x => x.KidId).NotEmpty().WithMessage("KidId is required");
    }
}
