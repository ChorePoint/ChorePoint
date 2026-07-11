using FluentValidation;

namespace ChorePoint.Application.Handlers.Shop.UpdateShopItem;

public class UpdateShopItemValidator : AbstractValidator<UpdateShopItemCommand>
{
    public UpdateShopItemValidator()
    {
        RuleFor(x => x.ShopItemId).NotEmpty().WithMessage("ShopItemId is required");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Icon).NotEmpty().WithMessage("Icon is required");

        RuleFor(x => x.Cost).NotEmpty().WithMessage("Cost is required");

        RuleFor(x => x.AssignedKids).NotEmpty().WithMessage("AssignedKids is required");

        RuleForEach(x => x.AssignedKids)
            .NotEmpty()
            .WithMessage("AssignedKids cannot contain a null element")
            .ChildRules(assignedKid =>
            {
                assignedKid
                    .RuleFor(x => x.KidId)
                    .NotEmpty()
                    .WithMessage("KidId is required in AssignedKids");
            });
    }
}
