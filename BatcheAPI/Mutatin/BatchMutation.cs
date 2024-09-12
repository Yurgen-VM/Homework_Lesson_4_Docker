using BatcheAPI.Abstractions;
using BatcheAPI.DB.DTO;

namespace BatcheAPI.Mutatin
{
    public class BatchMutation
    {
        public int AddSuppliers(PostSupplierDTO supplier, [Service] ISupplierRepository service)
        {
            var id = service.AddSupplier(supplier);
            return id;
        }

        public int AddProduct(PostProductDTO product, [Service] IProductRepository service)
        {
            var id = service.AddProduct(product);
            return id;
        }

        public int AddBatch(PostBatchDTO batch, [Service] IBatchRepository service)
        {
            var id = service.AddBatch(batch);
            return id;
        }

        public  int DelSupplier(int supplierId, [Service] ISupplierRepository service)
        {
            var id = service.DelSupplier(supplierId);
            return id;
        }

        public int DelProduct(int productId, [Service] IProductRepository service)
        {
            var id = service.DelProduct(productId);
            return id;
        }

        public int DelBatch(int batchId, [Service] IBatchRepository service)
        {
            var id = service.DelBatch(batchId);
            return id;
        }
    }
}
