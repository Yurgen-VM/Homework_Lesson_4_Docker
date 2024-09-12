using Market.Abstractions;
using Market.Models;
using Market.Models.DTO;
using Market.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using Path = System.IO.Path;

namespace Market.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StorageController: ControllerBase
    {
        IStorageRepository _storageRepository;
        public StorageController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        [HttpPost(template: "post_storage")]
        public ActionResult AddStorage([FromBody] PostStorageModel storageModel)
        {
            var result = _storageRepository.AddStorage(storageModel);
            return Ok(result);
        }

        [HttpGet(template: "get_storage")]
        public ActionResult<IEnumerable<StorageModel>> GetStorage()
        {
            var storage = _storageRepository.GetStorage();
            return Ok(storage);
        }

        [HttpGet(template: "get_storage_CSV")]
        public ActionResult<IEnumerable<StorageModel>> GetStorCSV()
        {
            var result = _storageRepository.GetStoragesCSV();
            return File(new System.Text.UTF32Encoding().GetBytes(result), "text/csv", "reportStorage.csv");
        }

        [HttpGet(template: "get_cache_statistic")]
        public ActionResult<MemoryCacheStatistics> GetCachStat()
        {
            var rsesult = _storageRepository.GetCacheStatistics();
            return Ok(rsesult);
        }

        [HttpGet(template: "get_cache_statistics_url")]
        public ActionResult<string> GetCacheStatisticsUrl()
        {
            var statistics = _storageRepository.GetCacheStatistics();
            if (statistics == null) return StatusCode(404);
            var sb = new StringBuilder();
            sb.AppendLine("\"Category\" Table;Cache Statistics");
            sb.AppendLine($"Current Entry Count;{statistics.CurrentEntryCount}");
            sb.AppendLine($"Current Estimated Size;{statistics.CurrentEstimatedSize}");
            sb.AppendLine($"Total Misses;{statistics.TotalMisses}");
            sb.AppendLine($"Total Hits;{statistics.TotalHits}");
            string fileName = $"categories_cache_stat_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
            System.IO.File.WriteAllText(path, sb.ToString());
            return $"{Request.Scheme}://{Request.Host}/static/{fileName}";
        }

        [HttpPut(template: "put_storage")]
        public ActionResult PutStorage([FromBody] StorageModel storageModel)
        {
            var result = _storageRepository.PutStorage(storageModel);
            return Ok(result);
        }

        [HttpDelete(template: "delete_storage")]
        public ActionResult DelStorage(DeleteModel delStorage)
        {
            var result = _storageRepository.DelStorage(delStorage);
            return Ok(result);
        }
    }
}
