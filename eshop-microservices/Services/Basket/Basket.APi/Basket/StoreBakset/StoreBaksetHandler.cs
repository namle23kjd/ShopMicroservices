
namespace Basket.APi.Basket.StoreBakset;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBaskeResult>;

public record StoreBaskeResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBaksetCommandHandle (IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBaskeResult>
{
    public async Task<StoreBaskeResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        await repository.StoreBasker(command.Cart, cancellationToken);
        return new StoreBaskeResult(command.Cart.UserName);
    }
}
