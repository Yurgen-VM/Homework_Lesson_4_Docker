using Lesson_3_GraphQL.Models.DTO;
using Market.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_3_GraphQL.Controllers
{
    [ApiController]
    [Route("Storage/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;


        public CategoryController(ICategoryRepository categotyRepository)
        {
            _categoryRepository = categotyRepository;
        }

        [HttpPost(template: "post_category")]
        public ActionResult AddСategory([FromBody] PostCategoryDTO categoryModel)
        {
            var result = _categoryRepository.AddCategory(categoryModel);
            return Ok(result);
        }

        [HttpGet(template: "get_category")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategory()
        {
            var result = _categoryRepository.GetCategories();
            return Ok(result);
        }      
                
    }
}
