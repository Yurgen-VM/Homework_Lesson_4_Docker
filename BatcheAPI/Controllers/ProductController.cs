using BatcheAPI.Abstractions;
using BatcheAPI.DB.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BatcheAPI.Controllers
{
    [ApiController]
    [Route("Batch/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost(template: "add_product")]
        public ActionResult AddProduct(PostProductDTO productDTO)
        {
            var result = _productRepository.AddProduct(productDTO);
            return Ok(result);
        }


        [HttpGet(template: "get_product")]
        public ActionResult<IEnumerable<ProductDTO>> GetProduct()
        {
            var result = _productRepository.GetProducts();
            return Ok(result);
        }

        [HttpDelete(template: "delete_product")]
        public ActionResult DelProduct(int productId)
        {
            var result = _productRepository.DelProduct(productId);
            return Ok(result);
        }
    }
}
