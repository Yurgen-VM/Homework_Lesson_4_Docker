using AutoMapper;
using BatcheAPI.Abstractions;
using BatcheAPI.DB;
using BatcheAPI.DB.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace BatcheAPI.Repo
{
    public class BatchRepo : IBatchRepository
    {
        public readonly BatchContext _context;
        public readonly IMapper _mapper;
        public readonly IMemoryCache _cache;

        public BatchRepo(BatchContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddBatch(PostBatchDTO batch)
        {
            var batchEntity = _context.Batches.FirstOrDefault(p => p.Number == batch.Number);

            if (batchEntity == null)
            {
                batchEntity = _mapper.Map<Batch>(batch);
                _context.Add(batchEntity);
                _context.SaveChanges();
                _cache.Remove("batch");
            }
            return batchEntity.Id;
        }

        public IEnumerable<BatchDTO> GetBatches()
        {
            if (_cache.TryGetValue("batch", out List<BatchDTO>? listBatches))
            {
                return listBatches ?? Enumerable.Empty<BatchDTO>();
            }

            var batchEntity = _context.Batches.Select(p => _mapper.Map<BatchDTO>(p));
            _cache.Set("batch", batchEntity, TimeSpan.FromMinutes(30));
            return batchEntity;
        }

        public int DelBatch(int batchId)
        {
            var entityBatch = _context.Batches.FirstOrDefault(x => x.Id.Equals(batchId));

            if (entityBatch != null)
            {
                _context.Batches.Remove(entityBatch);
                _context.SaveChanges();
                _cache.Remove("batch");
                return entityBatch.Id;
            }
            return -1;
        }
    }
}
