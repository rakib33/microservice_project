using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
     private readonly IDistributedCache _radisCache;
        public BasketRepository(IDistributedCache radisCache)
        {
            _radisCache = radisCache;
        }


        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _radisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return  null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _radisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }


        public async Task DeleteBasket(string userName)
        {
            await _radisCache.RemoveAsync(userName);
        }
    }
}
