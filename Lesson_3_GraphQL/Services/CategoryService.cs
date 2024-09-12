using AutoMapper;
using Lesson_3_GraphQL.Models;
using Lesson_3_GraphQL.Models.DTO;
using Market.Abstractions;
using Microsoft.Extensions.Caching.Memory;


namespace Lesson_3_GraphQL.Repo
{
    public class CategoryService : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly MarketContext _context;

        public CategoryService(MarketContext context, IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        public int AddCategory(PostCategoryDTO categoryDto)
        {
            var entityCategory = _context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals(categoryDto.Name.ToLower()));
            if (entityCategory == null)
            {
                entityCategory = _mapper.Map<Category>(categoryDto);
                _context.Categories.Add(entityCategory);
                _context.SaveChanges();
                _cache.Remove("categories");
            }
            return entityCategory.Id;

        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            if (_cache.TryGetValue("categories", out List<CategoryDTO>? cacheCategories))
            {
                return cacheCategories ?? Enumerable.Empty<CategoryDTO>();
            }

            var listCategory = _context.Categories.Select(c => _mapper.Map<CategoryDTO>(c)).ToList();
            _cache.Set("categories", listCategory, TimeSpan.FromMinutes(30));
            return listCategory;

        }
    }
}
