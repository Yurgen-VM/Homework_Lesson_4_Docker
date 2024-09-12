using Lesson_3_GraphQL.Models.DTO;

namespace Market.Abstractions
{
    public interface ICategoryRepository
    {
        public int AddCategory(PostCategoryDTO category);
        public IEnumerable<CategoryDTO> GetCategories();        
    }
}
