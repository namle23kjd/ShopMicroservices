namespace Basket.APi.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken =default );
    Task<ShoppingCart> StoreBasker(ShoppingCart basket, CancellationToken cancellationToken = default );

    Task<bool> DeleteBasler(string userName, CancellationToken cancellationToken = default);
}
