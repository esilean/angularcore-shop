using System;
using System.Text.Json;
using System.Threading.Tasks;
using AngularShop.Core.Entities;
using AngularShop.Core.Interfaces.Repositories;
using StackExchange.Redis;

namespace AngularShop.Infra.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<Basket> GetAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(data);
        }

        public async Task<Basket> UpdateAsync(Basket basket)
        {
            var created = await _database.StringSetAsync(
                                                        key: basket.Id,
                                                        value: JsonSerializer.Serialize(basket),
                                                        expiry: TimeSpan.FromMinutes(120));
            if (!created) return null;

            return await GetAsync(basket.Id);
        }

        public async Task<bool> RemoveAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }
    }
}