using Market.Models.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Abstractions
{
    public interface ICategoryRepository: IRepository
    {
        public int AddCategory(PostCategoryModel category);
        public IEnumerable<CategoryModel> GetCategories();
        public int DeleteCategory(DeleteModel category);
        public int PutCategory(CategoryModel category);
        public string GetCategoriesCSV();
    }
}
