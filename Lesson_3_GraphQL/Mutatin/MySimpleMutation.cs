using Lesson_3_GraphQL.Abstractions;
using Lesson_3_GraphQL.Models.DTO;
using Market.Abstractions;

namespace Lesson_3_GraphQL.Mutatin
{
    public class MySimpleMutation
    {
        public int AddProducts(PostProductDTO product, [Service] IProductRepository service)
        {
            var id = service.AddProduct(product);
            return id;
        }

        public int AddCategories(PostCategoryDTO category, [Service] ICategoryRepository service)
        {
            var id = service.AddCategory(category);
            return id;
        }

        public int AddStorage(PostStorageDTO storage, [Service] IStorageRepository service)
        {
            var id = service.AddStorage(storage);
            return id;
        }

        public int AddStorehouse (PostStorehouseDTO storehouse, [Service] IStorehouseRepository service)
        {
            var id = service.AddStorehouse(storehouse);
            return id;
        }
    }
}
