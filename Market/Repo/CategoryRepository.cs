using AutoMapper;
using Market.Abstractions;
using Market.Models;
using Market.Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Market.Repo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly string _connectionString;

        public CategoryRepository(IMapper mapper, IMemoryCache cache, string connectionString)
        {
            _mapper = mapper;
            _cache = cache;
            _connectionString = connectionString;
        }

        public int AddCategory(PostCategoryModel category)
        {

            using (var context = new MarketContext(_connectionString))
            {
                var entityCategory = context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals(category.Name.ToLower()));
                if (entityCategory == null)
                {
                    entityCategory = _mapper.Map<Category>(category);
                    context.Categories.Add(entityCategory);
                    context.SaveChanges();
                    _cache.Remove("categories");
                }
                return entityCategory.Id;
            }
        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            if (_cache.TryGetValue("categories", out List<CategoryModel>? cacheCategories))
            {
                return cacheCategories ?? Enumerable.Empty<CategoryModel>();
            }
            using (var context = new MarketContext(_connectionString))
            {
                var listCategory = context.Categories.Select(c => _mapper.Map<CategoryModel>(c)).ToList();
                _cache.Set("categories", listCategory, TimeSpan.FromMinutes(30));
                return listCategory;
            }
        }

        public MemoryCacheStatistics GetCacheStatistics()
        {
            return _cache.GetCurrentStatistics()!;
        }

        public int PutCategory(CategoryModel categoryModel)
        {
            using (var context = new MarketContext(_connectionString))
            {
                var entityCategory = context.Categories.FirstOrDefault(x => x.Id.Equals(categoryModel.Id));

                if (categoryModel is null)
                {
                    entityCategory = _mapper.Map<Category>(categoryModel);
                    context.Categories.Add(entityCategory);
                }
                else
                {
                    entityCategory!.Name = categoryModel.Name;
                    entityCategory.Description = categoryModel.Description;
                }

                context.SaveChanges();
                _cache.Remove("categories");
                return entityCategory.Id;
            }
        }

        public int DeleteCategory(DeleteModel category)
        {
            using (var context = new MarketContext(_connectionString))
            {
                var entityCategory = context.Categories.FirstOrDefault(x => x.Id.Equals(category.Id));

                if (entityCategory != null)
                {
                    context.Categories.Remove(entityCategory);
                    context.SaveChanges();
                    _cache.Remove("categories");
                    return entityCategory.Id;
                }
                return -1;
            }

        }

        public string GetCategoriesCSV()
        {
            IEnumerable<CategoryModel> listCategories = GetCategories();
            StringBuilder cb = new StringBuilder();
            cb.Append($"Id ; Name\n");
            foreach (var category in listCategories)
            {
                cb.Append($"{category.Id} ; {category.Name} \n");
            }
            return cb.ToString();

        }
    }
}
