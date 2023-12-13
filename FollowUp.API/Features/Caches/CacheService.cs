using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.Caches
{
    public class CacheService : ICacheService
    {
        private static ConcurrentDictionary<string, bool> _cacheKeys = new ();
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger _logger;

        public CacheService(IDistributedCache distributedCache, ILogger logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(
            string key, 
            CancellationToken cancellationToken = default) where T : class
        {
            try
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
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to get cached value for key {@cacheKey}", 
                    key);
                
                return null;
            }
        }

        public async Task SetAsync<T>(
            string key,
            T value,
            CancellationToken cancellationToken = default) where T : class
        {
            try
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
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to set cached value for key {@cacheKey}", 
                    key);
            }
        }

        public async Task RemoveAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _distributedCache.RemoveAsync(
                    key, 
                    cancellationToken);

                _cacheKeys.TryRemove(key, out _);
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to remove cached value for key {@cacheKey}", 
                    key);
            }
        }

        public async Task<T?> GetAsync<T>(
            string key, 
            Func<Task<T>> factory, 
            CancellationToken cancellationToken = default) where T : class
        {
            try
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
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to get cached value for key {@cacheKey} on factory overload", 
                    key);
                
                return null;
            }
        }

        public async Task RemoveByPrefixAsync(
            string prefixKey, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                IEnumerable<Task> tasks = _cacheKeys
                    .Keys
                    .Where(key => key.StartsWith(prefixKey))
                    .Select(key => this.RemoveAsync(
                        key, 
                        cancellationToken));

                await Task.WhenAll(tasks);
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to remove cached values for prefix {@cachePrefixKey}", 
                    prefixKey);
            }
        }
    }
}
