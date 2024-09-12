using AutoMapper;
using Lesson_3_GraphQL.Abstractions;
using Lesson_3_GraphQL.Models;
using Lesson_3_GraphQL.Models.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace Lesson_3_GraphQL.Services
{
    public class StorehouseService : IStorehouseRepository
    {
        public readonly IMapper _mapper;
        public readonly MarketContext _context;
        public readonly IMemoryCache _cache;
        private string cacheKey = "storehouse";

        public StorehouseService(MarketContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }


        public int AddStorehouse(PostStorehouseDTO storehouse)
        {
            var storehouseEntity = _context.Storehouse.FirstOrDefault(e => e.Name.ToLower().Equals(storehouse.Name.ToLower()));
            if (storehouseEntity == null)
            {
                storehouseEntity = _mapper.Map<Storehouse>(storehouse);
                _context.Add(storehouseEntity);
                _context.SaveChanges();
                _cache.Remove("storehouse");
            }
            return storehouseEntity.Id;
        }

        public IEnumerable<StorehouseDTO> GetStorehouse()
        {
            if (_cache.TryGetValue("storehouse", out List<StorehouseDTO>? shList))
            {
                return shList ?? Enumerable.Empty<StorehouseDTO>();
            }

            var stourhouses = _context.Storehouse.Select(s => _mapper.Map<StorehouseDTO>(s)).ToList();
            _cache.Set("storehouse", stourhouses, TimeSpan.FromMinutes(30));
            return stourhouses;
        }

        public IEnumerable<StorageDTO> GetProductsFromStorehouse(int IdStorehouse)
        {

            cacheKey = $"storehouse_{IdStorehouse}";

            if (_cache.TryGetValue(cacheKey, out List<StorageDTO>? storage))
                return storage ?? Enumerable.Empty<StorageDTO>();

            var listProducts = _context.Storages
                .Where(s => s.StorehouseId == IdStorehouse)               
                .Select(s => _mapper.Map<StorageDTO>(s)).ToList();

            _cache.Set(cacheKey, listProducts, TimeSpan.FromMinutes(30));
            return listProducts;
        }
    }
}
