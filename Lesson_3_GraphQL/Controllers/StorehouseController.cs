using Lesson_3_GraphQL.Abstractions;
using Lesson_3_GraphQL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_3_GraphQL.Controllers
{
    [ApiController]
    [Route("Storage/[controller]")]
    public class StorehouseController: ControllerBase
    {
        private readonly IStorehouseRepository _storehouseRepository;
        
        public StorehouseController(IStorehouseRepository storehouseRepository )
        {
            _storehouseRepository = storehouseRepository;
        }

        [HttpGet(template: "get_product_from_storehouse")]
        public ActionResult <IEnumerable <StorageDTO>> GetProductsFromStorehouse(int storehouseId)
        {
            var result = _storehouseRepository.GetProductsFromStorehouse(storehouseId);
            return Ok(result);
        }
    }
}
