﻿using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _radisCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            this._radisCache = distributedCache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _radisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await _radisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
            return await GetBasket(shoppingCart.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
            await _radisCache.RemoveAsync(userName);
        }
    }
}
