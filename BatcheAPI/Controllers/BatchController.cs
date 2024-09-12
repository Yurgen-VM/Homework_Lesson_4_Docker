using BatcheAPI.Abstractions;
using BatcheAPI.DB.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BatcheAPI.Controllers
{
    [ApiController]
    [Route("Batch/[controller]")]
    public class BatchController: ControllerBase
    {
        private readonly IBatchRepository _batchRepository;

        public BatchController(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        [HttpPost(template: "add_batch")]
        public ActionResult AddBatch(PostBatchDTO batchDTO)
        {
            var result = _batchRepository.AddBatch(batchDTO);
            return Ok(result);
        }


        [HttpGet(template: "get_batch")]
        public ActionResult<IEnumerable<BatchDTO>> GetBatches()
        {
            var result = _batchRepository.GetBatches();
            return Ok(result);
        }

        [HttpDelete(template: "delete_batch")]
        public ActionResult DelBatch(int batchId)
        {
            var result = _batchRepository.DelBatch(batchId);
            return Ok(result);
        }
    }
}
