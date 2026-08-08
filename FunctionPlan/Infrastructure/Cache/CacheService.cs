using Application.Abstractions.Cache;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Cache
{
    internal sealed class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;   

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

            if(cachedValue is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(cachedValue);

        }


        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            //Serialize new value
            //Save value with key

            var serializedValue = JsonSerializer.Serialize(value);

            var options = new DistributedCacheEntryOptions();

            if(expiration.HasValue)
            {
                options.SetAbsoluteExpiration(expiration.Value);
            }

            await _distributedCache.SetStringAsync(key, serializedValue, options, cancellationToken);
        }



        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }

    }
}
