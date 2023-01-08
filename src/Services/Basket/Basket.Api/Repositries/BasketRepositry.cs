using Basket.Api.Entity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.Api.Repositries
{
    public class BasketRepositry : IBasketRepositry
    {
        private readonly IDistributedCache _redisCash;
        public BasketRepositry(IDistributedCache redisCash)
        {
            _redisCash = redisCash ?? throw new ArgumentNullException(nameof(redisCash));
        }
        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCash.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return new ShoppingCart(userName);

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCash.SetStringAsync(basket.UserName,JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
            await _redisCash.RemoveAsync(userName);
        }

    }
}
