using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xend.AppService.MemoryService
{
    public interface IMemoryCacheService
    {
        Task<T> AddNewItem<T>(T model, string key);
        Task<T> UpdateItem<T>(T model, string key);
        Task<T> Get<T>(string key, string searchValue);
        Task<List<T>> GetAll<T>(string key, string search);
    }
}