using Newtonsoft.Json;
using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Repositories;

public class BasketRepository(IDistributedCache cache) : IBasketRepository
{
    private readonly IDistributedCache cache = cache;

    public async Task DeleteBasket(string userName)
    {
        await cache.RemoveAsync(userName);
    }

    public async Task<ShoppingCart?> GetUserBasket(string userName)
    {
        var basket = await cache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
            return default;

        return JsonConvert.DeserializeObject<ShoppingCart>(basket)!;
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart cart)
    {
        await cache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
        return await GetUserBasket(cart.UserName);
    }
}