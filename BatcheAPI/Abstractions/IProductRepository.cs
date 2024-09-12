using BatcheAPI.DB.DTO;

namespace BatcheAPI.Abstractions
{
    public interface IProductRepository
    {
        public int AddProduct(PostProductDTO product);
        public IEnumerable<ProductDTO> GetProducts();
        public int DelProduct(int productId);
    }
}
