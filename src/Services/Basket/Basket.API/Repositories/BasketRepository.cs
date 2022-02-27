using Basket.API.Entities;
using Basket.API.GrpcServices;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketRepository(IDistributedCache redisCache, DiscountGrpcService discountGrpcService)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {

            foreach (var item in basket.Items)
            {
                var discount = await _discountGrpcService.GetDiscount(item.ProductName);

                item.Price -= discount.Amount;
            }

            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
