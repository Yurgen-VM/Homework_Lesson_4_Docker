using AutoMapper;
using Market.Abstractions;
using Market.Models;
using Market.Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Market.Repo
{
    public class ProductRepository : IProductRepository
    {

        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly string _connectionString;

        public ProductRepository(IMapper mapper, IMemoryCache cache, string connectionString)
        {
            _mapper = mapper;
            _cache = cache;
            _connectionString = connectionString;
        }


        public int AddProduct(PostProductModel product)
        {
                     
            using (var context = new MarketContext(_connectionString))
            {
                try
                {
                    var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(product.Name.ToLower()));
                    if (entityProduct == null)
                    {
                        entityProduct = _mapper.Map<Product>(product);
                        context.Products.Add(entityProduct);
                        context.SaveChanges();
                        _cache.Remove("products");
                    }
                    return entityProduct.Id;
                }
                catch 
                {
                    return -1;
                }                
            }
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            if(_cache.TryGetValue("products", out List<ProductModel>?products))
            {
                return products ?? Enumerable.Empty<ProductModel>();
            }
            
            using (var context = new MarketContext(_connectionString))
            {
                var listProduct = context.Products.Select(p => _mapper.Map<ProductModel>(p)).ToList();
                _cache.Set("products", listProduct, TimeSpan.FromMinutes(30));
                return listProduct;
            }
        }

        public MemoryCacheStatistics GetCacheStatistics()
        {
            return _cache.GetCurrentStatistics()!;
        }

        public int DelProduct(DeleteModel product)
        {
            using (var context = new MarketContext(_connectionString))
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Id.Equals(product.Id));

                if (entityProduct != null)
                {
                    context.Products.Remove(entityProduct);
                    context.SaveChanges();
                    _cache.Remove("products");
                    return entityProduct.Id;
                }
                return -1;
            }
        }

        public string GetProductsCSV()
        {
            IEnumerable<ProductModel> listProducts = GetProducts();
            StringBuilder cb = new StringBuilder();
            cb.Append($"Id ; Name\n");
            foreach (var product in listProducts)
            {
                cb.Append($"{product.Id} ; {product.Name} \n");
            }
            return cb.ToString();
        }


        public int PutProduct(ProductModel productModel)
        {
            using (var context = new MarketContext(_connectionString))
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Id.Equals(productModel.Id));

                if (productModel is null)
                {
                    entityProduct = _mapper.Map<Product>(productModel);
                    context.Products.Add(entityProduct);
                }
                else
                {
                    entityProduct!.Name = productModel.Name;
                    entityProduct.Description = productModel.Description;
                    entityProduct.Price = productModel.Price;
                    entityProduct.CategoryId = productModel.CategoryId;
                }

                context.SaveChanges();
                _cache.Remove("products");
                return entityProduct.Id;
            }
        }

        public int PatchProduct(PriceProductModel priceProductModel)
        {
            using (var context = new MarketContext(_connectionString))
            {
                var product = context.Products.FirstOrDefault(x => x.Id.Equals(priceProductModel.Id));

                if (product == null)
                {
                    return -1;
                }
                product.Price = priceProductModel.Price;
                context.SaveChanges();
                _cache.Remove("products");
                return priceProductModel.Id;
            }
        }       
    }
}
