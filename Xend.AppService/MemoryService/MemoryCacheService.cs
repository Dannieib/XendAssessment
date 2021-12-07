using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xend.AppService.MemoryService
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private IMemoryCache _cache;
        public static readonly string UrlShortenerKey = "UrlShortenerKey";
        public static readonly string UrlShortenerStat = "UrlShortenerStat";

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<T> AddNewItem<T>(T model, string key)
        {
            try
            {
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(2),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(10),
                    Size = 1024,
                };

                var resp = _cache.Set(key, model, cacheExpiryOptions);
                return await Task.FromResult(resp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> UpdateItem<T>(T model, string key)
        {
            try
            {
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(2),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(10),
                    Size = 1024,
                };

                var resp = _cache.Set(key, model, cacheExpiryOptions);
                return await Task.FromResult(resp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> Get<T>(string key, string searchValue)
        {
            try
            {
                var get = _cache.Get<T>(key);
                return get;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<T>> GetAll<T>(string key, string search)
        {
            try
            {
                var get = _cache.Get<List<T>>(key);
                return get;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
