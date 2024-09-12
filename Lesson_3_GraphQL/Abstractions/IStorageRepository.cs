using Lesson_3_GraphQL.Models.DTO;

namespace Market.Abstractions
{
    public interface IStorageRepository
    {
        public int AddStorage(PostStorageDTO product);
        public IEnumerable<StorageDTO> GetStorage();
        public IEnumerable<StorageDTO> GetStorageById(int IdProduct);
        public IEnumerable<ProductDTO> GetProductFromStorage(int IdStorage);
    }
}
