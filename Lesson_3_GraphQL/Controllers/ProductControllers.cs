using Lesson_3_GraphQL.Abstractions;
using Lesson_3_GraphQL.Models.DTO;
using Microsoft.AspNetCore.Mvc;


namespace Lesson_3_GraphQL.Controllers
{
    [ApiController]
    [Route("Storage/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository producctRepositoty)
        {
            _productRepository = producctRepositoty;
        }

        [HttpPost(template: "post_product")]
        public ActionResult AddProduct([FromBody] PostProductDTO productModel)
        {
            var result = _productRepository.AddProduct(productModel);
            return Ok(result);
        }

        [HttpGet(template: "get_products")]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }
    }       
}
