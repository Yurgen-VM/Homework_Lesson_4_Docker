using BatcheAPI.DB.DTO;

namespace BatcheAPI.Abstractions
{
    public interface ISupplierRepository
    {
        public int AddSupplier (PostSupplierDTO supplier);
        public IEnumerable <SupplierDTO> GetSuppliers();
        public int DelSupplier(int suppplierId);
    }
}
