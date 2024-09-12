using AutoMapper;
using BatcheAPI.Abstractions;
using BatcheAPI.DB;
using BatcheAPI.DB.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace BatcheAPI.Repo
{
    public class ProductRepo : IProductRepository
    {

        public readonly BatchContext _context;
        public readonly IMapper _mapper;
        public readonly IMemoryCache _cache;

        public ProductRepo(BatchContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddProduct(PostProductDTO product)
        {
            var productEntity = _context.Products.FirstOrDefault(p => p.Name == product.Name);

            if (productEntity == null)
            {
                productEntity = _mapper.Map<Product>(product);
                _context.Add(productEntity);
                _context.SaveChanges();
                _cache.Remove("product");
            }
            return productEntity.Id;
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            if (_cache.TryGetValue("product", out List<ProductDTO>? listProducts))
            {
                return listProducts ?? Enumerable.Empty<ProductDTO>();
            }

            var productEntity = _context.Products.Select(p => _mapper.Map<ProductDTO>(p));
            _cache.Set("product", productEntity, TimeSpan.FromMinutes(30));
            return productEntity;
        }

        public int DelProduct(int productId)
        {
            var entityProduct = _context.Products.FirstOrDefault(x => x.Id.Equals(productId));

            if (entityProduct != null)
            {
                _context.Products.Remove(entityProduct);
                _context.SaveChanges();
                _cache.Remove("product");
                return entityProduct.Id;
            }
            return -1;
        }
    }
}
