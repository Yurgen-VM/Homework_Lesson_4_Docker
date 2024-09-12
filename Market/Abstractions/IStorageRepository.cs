using Market.Models.DTO;

namespace Market.Abstractions
{
    public interface IStorageRepository: IRepository
    {
        public int AddStorage(PostStorageModel product);
        public IEnumerable<StorageModel> GetStorage();
        public int DelStorage(DeleteModel product);
        public int PutStorage(StorageModel product);
        public string GetStoragesCSV();
    }
}
