using Market.Models.DTO;

namespace Market.Abstractions
{
    public interface IProductRepository: IRepository
    {
        public int AddProduct(PostProductModel product);
        public IEnumerable<ProductModel> GetProducts();
        public int DelProduct(DeleteModel product);
        public int PutProduct(ProductModel product);
        public int PatchProduct(PriceProductModel product);
        public string GetProductsCSV();
    }
}
