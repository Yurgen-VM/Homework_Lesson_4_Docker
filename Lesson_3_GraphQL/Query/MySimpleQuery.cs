using Lesson_3_GraphQL.Abstractions;
using Lesson_3_GraphQL.Models.DTO;
using Market.Abstractions;

namespace Lesson_3_GraphQL.Query
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductDTO> GetProducts([Service] IProductRepository service) => service.GetProducts();
        public IEnumerable<StorageDTO> GetStorage([Service] IStorageRepository service) => service.GetStorage();
        public IEnumerable<CategoryDTO> GetCategories([Service] ICategoryRepository service) => service.GetCategories();
        public IEnumerable<StorageDTO> GetStorageByIdProduct(int idProduct, [Service] IStorageRepository service) => service.GetStorageById(idProduct);
        public IEnumerable<ProductDTO> GetProductFromStorage(int idStorage, [Service] IStorageRepository service) => service.GetProductFromStorage(idStorage);
        public IEnumerable<StorehouseDTO> GetStorehouses([Service] IStorehouseRepository service) => service.GetStorehouse();
        public IEnumerable<StorageDTO> GetProductFromStorehouse(int idStorehouse, [Service] IStorehouseRepository service) => service.GetProductsFromStorehouse(idStorehouse);
    }
}
