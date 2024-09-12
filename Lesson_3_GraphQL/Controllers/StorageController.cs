using Azure.Core;
using Lesson_3_GraphQL.Models.DTO;
using Market.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Lesson_3_GraphQL.Controllers
{
    [ApiController]
    [Route("Storage/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageRepository _storageRepository;
        public StorageController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        [HttpPost(template: "post_storage")]
        public ActionResult AddStorage([FromBody] PostStorageDTO storageModel)
        {
            var result = _storageRepository.AddStorage(storageModel);
            return Ok(result);
        }

        [HttpGet(template: "get_storage")]
        public ActionResult<IEnumerable<StorageDTO>> GetStorage()
        {
            var storage = _storageRepository.GetStorage();
            return Ok(storage);
        }        
    }
}
