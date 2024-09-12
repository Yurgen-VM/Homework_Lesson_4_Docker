using BatcheAPI.Abstractions;
using BatcheAPI.DB.DTO;

namespace BatcheAPI.Query
{
    public class BatchQuery
    {
        public IEnumerable<SupplierDTO> GetSuppliers([Service] ISupplierRepository service) => service.GetSuppliers();
        public IEnumerable<ProductDTO> GetProducts([Service] IProductRepository service) => service.GetProducts();
        public IEnumerable<BatchDTO> GetBatches([Service] IBatchRepository service) => service.GetBatches();
    }
}
