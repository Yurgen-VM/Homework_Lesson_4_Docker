using AutoMapper;
using Lesson_3_GraphQL.Abstractions;
using Lesson_3_GraphQL.Models;
using Lesson_3_GraphQL.Models.DTO;
using Microsoft.Extensions.Caching.Memory;


namespace Lesson_3_GraphQL.Repo
{
    public class ProductService : IProductRepository
    {

        private readonly MarketContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductService(MarketContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }


        public int AddProduct(PostProductDTO productDto)
        {
            try
            {
                var entityProduct = _context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(productDto.Name.ToLower()));
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Product>(productDto);
                    _context.Products.Add(entityProduct);
                    _context.SaveChanges();
                    _cache.Remove("products");
                }
                return entityProduct.Id;
            }
            catch
            {
                return -1;
            }

        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDTO>? products))
            {
                return products ?? Enumerable.Empty<ProductDTO>();
            }

            var listProduct = _context.Products.Select(p => _mapper.Map<ProductDTO>(p)).ToList();
            _cache.Set("products", listProduct, TimeSpan.FromMinutes(30));
            return listProduct;

        }
    }
}
