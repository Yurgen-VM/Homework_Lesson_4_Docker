using Market.Abstractions;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;



        public CategoryController(ICategoryRepository categotyRepository)
        {
            _categoryRepository = categotyRepository;
        }

        [HttpPost(template: "post_category")]
        public ActionResult AddСategory([FromBody] PostCategoryModel categoryModel)
        {
            var result = _categoryRepository.AddCategory(categoryModel);
            return Ok(result);
        }

        [HttpGet(template: "get_category")]
        public ActionResult<IEnumerable<CategoryModel>> GetCategory()
        {
            var result = _categoryRepository.GetCategories();
            return Ok(result);
        }              

        [HttpGet(template: "get_catigories_CSV")]
        public ActionResult<IEnumerable<CategoryModel>> GetCatCSV()
        {
            var result = _categoryRepository.GetCategoriesCSV();
            return File(new System.Text.UTF32Encoding().GetBytes(result),"text/csv","reportCategories.csv");
        }

        [HttpGet(template: "get_cache_statistic")]
        public ActionResult<MemoryCacheStatistics> GetCachStat()
        {
            var result = _categoryRepository.GetCacheStatistics();
            return Ok(result);
        }

        [HttpGet(template: "get_cache_statistics_url")]
        public ActionResult<string> GetCacheStatisticsUrl()
        {
            var statistics = _categoryRepository.GetCacheStatistics();
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


        [HttpPut(template: "put_category")]
        public ActionResult PutCategory([FromBody] CategoryModel categoryModel)
        {
            var result = _categoryRepository.PutCategory(categoryModel);
            return Ok(result);
        }

        [HttpDelete(template: "delete_category")]
        public ActionResult DeleteCategory(DeleteModel postCategoryModel)
        {
            var result = _categoryRepository.DeleteCategory(postCategoryModel);
            return Ok(result);
        }
    }
}
