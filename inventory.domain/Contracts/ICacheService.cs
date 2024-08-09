using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.domain.Contracts
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchFunction, TimeSpan absoluteExpirationRelativeToNow);
        void Remove(string cacheKey);
    }
}
