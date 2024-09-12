using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(template:"get_version")]

        public  ActionResult GetVersion()
        {
            var version = _configuration.GetSection("VersionInfo").Get<VersioInfo>();
            return Ok(version?.Lesson); //Можем получить конкретный папарметр секции VersionInfo или целиком секцию, если не укажем свойство
        }
    }
}
