using BatcheAPI.DB.DTO;

namespace BatcheAPI.Abstractions
{
    public interface IBatchRepository
    {
        public int AddBatch(PostBatchDTO batch);
        public IEnumerable<BatchDTO> GetBatches();
        public int DelBatch(int batchId);
    }
}

