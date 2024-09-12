using AutoMapper;
using Market.Abstractions;
using Market.Models;
using Market.Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Market.Repo
{
    public class StorageRepository : IStorageRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly string _connectionString;

        public StorageRepository(IMapper mapper, IMemoryCache cache, string connectionString)
        {
            _mapper = mapper;
            _cache = cache;
            _connectionString = connectionString;
        }

        public int AddStorage(PostStorageModel storage)
        {
            using (var context = new MarketContext(_connectionString))
            {
                var entityStorage = context.Storages.FirstOrDefault(x => x.Name.ToLower().Equals(storage.Name.ToLower()));
                if (entityStorage == null)
                {
                    entityStorage = _mapper.Map<Storage>(storage);
                    context.Storages.Add(entityStorage);                   
                    context.SaveChanges();
                    _cache.Remove("stоrages");
                }
                return entityStorage.Id;
            }
        }

        public int DelStorage(DeleteModel storage)
        {
            using (var context = new MarketContext(_connectionString))
            {
                var entityStorage = context.Storages.FirstOrDefault(x => x.Id.Equals(storage.Id));

                if (entityStorage != null)
                {
                    context.Storages.Remove(entityStorage);
                    context.SaveChanges();
                    _cache.Remove("storages");
                    return entityStorage.Id;
                }
                return -1;
            }
        }

        public IEnumerable<StorageModel> GetStorage()
        {
            if (_cache.TryGetValue("storages", out List<StorageModel>? storages))
                return storages ?? Enumerable.Empty<StorageModel>() ;

            using (var context = new MarketContext(_connectionString))
            {
                var listStorage = context.Storages.Select(p => _mapper.Map<StorageModel>(p)).ToList();
                _cache.Set("storages", listStorage, TimeSpan.FromMinutes(30));
                return listStorage;
            }
        }

        public MemoryCacheStatistics GetCacheStatistics()
        {
            return _cache.GetCurrentStatistics()!;
        }

        public int PutStorage(StorageModel storageModel)
        {
            using (var context = new MarketContext(_connectionString))
            {
                var entityStorage = context.Storages.FirstOrDefault(x => x.Id.Equals(storageModel.Id));

                if (storageModel is null)
                {
                    entityStorage = _mapper.Map<Storage>(storageModel);
                    context.Storages.Add(entityStorage);
                }
                else
                {
                    entityStorage!.Name = storageModel.Name;
                    entityStorage.Description = storageModel.Description;
                    entityStorage.ProductId = storageModel.ProductId;
                    entityStorage.Quantity = storageModel.Quantity;
                }

                context.SaveChanges();
                _cache.Remove("storages");
                return entityStorage.Id;
            }
        }

        public string GetStoragesCSV()
        {
            IEnumerable<StorageModel> listStorages = GetStorage();
            StringBuilder cb = new StringBuilder();
            cb.Append($"Id ; Name\n");
            foreach (var category in listStorages)
            {
                cb.Append($"{category.Id} ; {category.Name} \n");
            }
            return cb.ToString();

        }
    }
}
