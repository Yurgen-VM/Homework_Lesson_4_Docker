using Lesson_3_GraphQL.Models;
using Lesson_3_GraphQL.Models.DTO;


namespace Lesson_3_GraphQL.Abstractions
{
    public interface IProductRepository
    {
        public IEnumerable<ProductDTO> GetProducts();
        public int AddProduct(PostProductDTO productDTO);        
    }
}
