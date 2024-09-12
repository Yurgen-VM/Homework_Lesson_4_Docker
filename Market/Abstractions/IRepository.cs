using Microsoft.Extensions.Caching.Memory;

namespace Market.Abstractions
{
    public interface IRepository
    {
        public MemoryCacheStatistics GetCacheStatistics();        
    }
}
