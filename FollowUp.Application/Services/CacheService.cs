using FollowUp.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Services
{
    public class CacheService : ICacheService
    {
        private static ConcurrentDictionary<string, bool> _cacheKeys = new ();
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(
            string key, 
            CancellationToken cancellationToken = default) where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(
                key, 
                cancellationToken);

            if (cachedValue is null)
            {
                return null;
            }

            T? value = JsonConvert.DeserializeObject<T>(cachedValue);

            return value;
        }

        public async Task SetAsync<T>(
            string key,
            T value,
            CancellationToken cancellationToken = default) where T : class
        {
            string serializedValue = JsonConvert.SerializeObject(value);

            await _distributedCache.SetStringAsync(
                key,
                serializedValue,
                new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(15),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                },
                cancellationToken);

            _cacheKeys.TryAdd(key, false);
        }

        public async Task RemoveAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(
                key, 
                cancellationToken);

            _cacheKeys.TryRemove(key, out _);
        }

        public async Task<T?> GetAsync<T>(
            string key, 
            Func<Task<T>> factory, 
            CancellationToken cancellationToken = default) where T : class
        {
            T? cachedValue = await this.GetAsync<T>(
                key,
                cancellationToken);

            if (cachedValue is not null)
            {
                return cachedValue;
            }

            cachedValue = await factory();

            await this.SetAsync<T>(
                key, 
                cachedValue, 
                cancellationToken);

            return cachedValue;
        }

        public async Task RemoveByPrefixAsync(
            string prefixKey, 
            CancellationToken cancellationToken = default)
        {
            IEnumerable<Task> tasks = _cacheKeys
                .Keys
                .Where(key => key.StartsWith(prefixKey))
                .Select(key => this.RemoveAsync(
                    key, 
                    cancellationToken));

            await Task.WhenAll(tasks);
        }
    }
}
