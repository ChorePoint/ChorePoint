using ChorePoint.Application.Handlers.Shop.BuyShopItem;
using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.DeleteShopItem;

public class DeleteShopItemValidator : AbstractValidator<BuyShopItemCommand>
{
    public DeleteShopItemValidator()
    {
        RuleFor(x => x.ShopItemId)
            .NotEmpty().WithMessage("ShopItemId is required");
    }
}