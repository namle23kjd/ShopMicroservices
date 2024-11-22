
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.APi.Data;

public class CacheBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    public async Task<bool> DeleteBasler(string userName, CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasler(userName, cancellationToken);
        await cache.RemoveAsync(userName, cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);
        if(!string.IsNullOrEmpty(cacheBasket))        
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
        
        var basker = await repository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basker), cancellationToken);
        return basker;
    }

    public async Task<ShoppingCart> StoreBasker(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasker(basket, cancellationToken);
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    } 
}
