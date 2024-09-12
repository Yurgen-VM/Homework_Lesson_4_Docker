using Lesson_3_GraphQL.Models.DTO;

namespace Lesson_3_GraphQL.Abstractions
{
    public interface IStorehouseRepository
    {
        public int AddStorehouse(PostStorehouseDTO storehouse);
        public IEnumerable<StorehouseDTO> GetStorehouse();
        public IEnumerable<StorageDTO> GetProductsFromStorehouse(int IdStorage);
    }
}
