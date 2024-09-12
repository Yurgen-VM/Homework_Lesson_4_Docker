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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository producctRepositoty)
        {
            _productRepository = producctRepositoty;
        }

        [HttpPost(template: "post_product")]
        public ActionResult AddProduct([FromBody] PostProductModel productModel)
        {
            var result = _productRepository.AddProduct(productModel);
            return Ok(result);
        }

        [HttpGet(template: "get_products")]
        public ActionResult<IEnumerable<ProductModel>> GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet(template: "get_products_CSV")]
        public ActionResult<IEnumerable<ProductModel>> GetProdCSV()
        {
            var result = _productRepository.GetProductsCSV();
            return File(new System.Text.UTF32Encoding().GetBytes(result), "text/csv", "reportProducts.csv");
        }

        [HttpGet(template: "get_cache_statistic")]
        public ActionResult<MemoryCacheStatistics> GetCachStat()
        {
            var rsesult = _productRepository.GetCacheStatistics();
            return Ok(rsesult);
        }

        [HttpGet(template: "get_cache_statistics_url")]
        public ActionResult<string> GetCacheStatisticsUrl()
        {
            var statistics = _productRepository.GetCacheStatistics();
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

        [HttpPut(template: "put_product")]
        public ActionResult PutProduct([FromBody] ProductModel productModel)
        {
            var result = _productRepository.PutProduct(productModel);
            return Ok(result);
        }

        [HttpPatch(template: "patch_product")]
        public ActionResult PatchProduct([FromBody] PriceProductModel productModel)
        {
            var result = _productRepository.PatchProduct(productModel);
            return Ok(result);
        }       

        [HttpDelete(template: "delete_product")]
        public ActionResult DelProduct(DeleteModel delProductModel)
        {
            var result = _productRepository.DelProduct(delProductModel);
            return Ok(result);
        }
    }
}
