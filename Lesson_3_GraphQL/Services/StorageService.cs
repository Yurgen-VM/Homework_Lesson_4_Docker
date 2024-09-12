using AutoMapper;
using Lesson_3_GraphQL.Models;
using Lesson_3_GraphQL.Models.DTO;
using Market.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace Lesson_3_GraphQL.Repo
{
    public class StorageService : IStorageRepository
    {
        private readonly MarketContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private string cacheKey = "storages";

        public StorageService(MarketContext context, IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        public int AddStorage(PostStorageDTO storageDto)
        {
            var entityStorage = _context.Storages.FirstOrDefault(x => x.StorehouseId == storageDto.StorehouseId && x.Number == storageDto.Number);
            if (entityStorage == null)
            {
                entityStorage = _mapper.Map<Storage>(storageDto);
                _context.Storages.Add(entityStorage);
                _context.SaveChanges();
                _cache.Remove("storages");
                _cache.Remove($"storehouse_{entityStorage.StorehouseId}");
            }
            return entityStorage.Id;

        }


        public IEnumerable<StorageDTO> GetStorage()
        {
            
            if (_cache.TryGetValue("storages", out List<StorageDTO>? storages))
                return storages ?? Enumerable.Empty<StorageDTO>();

            var listStorage = _context.Storages.Select(p => _mapper.Map<StorageDTO>(p)).ToList();
            _cache.Set("storages", listStorage, TimeSpan.FromMinutes(30));
            return listStorage;

        }

        public IEnumerable<StorageDTO> GetStorageById(int IdProduct)
        {
            cacheKey = $"storages_{IdProduct}";

            if (_cache.TryGetValue(cacheKey, out List<StorageDTO>? storages))
                return storages ?? Enumerable.Empty<StorageDTO>();

            var listStorage = _context.Storages
                .Where(p => p.ProductId == IdProduct)
                .Select(s => _mapper.Map<StorageDTO>(s))
                .ToList();

            _cache.Set(cacheKey, listStorage, TimeSpan.FromMinutes(30));
            return listStorage;
        }

        public IEnumerable<ProductDTO> GetProductFromStorage(int IdStorage)
        {
            cacheKey = $"protuct_storages_{IdStorage}";

            if (_cache.TryGetValue(cacheKey, out List<ProductDTO>? products))
                return products ?? Enumerable.Empty<ProductDTO>();

            var listProducts = _context.Storages
                .Where(s => s.Id == IdStorage)
                .Select(p => p.Products)
                .Select(p  => _mapper.Map<ProductDTO>(p)).ToList();

            _cache.Set(cacheKey, listProducts, TimeSpan.FromMinutes(30));
            return listProducts;
        }       
    }
}
