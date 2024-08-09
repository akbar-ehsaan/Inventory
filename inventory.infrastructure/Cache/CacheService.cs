using inventory.domain.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.infrastructure.Cache;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchFunction, TimeSpan absoluteExpirationRelativeToNow)
    {
        if (_memoryCache.TryGetValue(cacheKey, out T cachedValue))
        {
            return cachedValue;
        }

        var value = await fetchFunction();

        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
        };

        _memoryCache.Set(cacheKey, value, cacheOptions);

        return value;
    }

    public void Remove(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);
    }
}

