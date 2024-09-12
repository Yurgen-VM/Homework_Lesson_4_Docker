using AutoMapper;
using BatcheAPI.Abstractions;
using BatcheAPI.DB;
using BatcheAPI.DB.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace BatcheAPI.Repo
{
    public class SupplierRepo : ISupplierRepository
    {
        public readonly BatchContext _context;
        public readonly IMapper _mapper;
        public readonly IMemoryCache _cache;

        public SupplierRepo(BatchContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddSupplier(PostSupplierDTO supplier)
        {
            var supplierEntity = _context.Suppliers.FirstOrDefault(p => p.Name == supplier.Name);

            if (supplierEntity == null)
            {
                supplierEntity = _mapper.Map<Supplier>(supplier);
                _context.Add(supplierEntity);
                _context.SaveChanges();
                _cache.Remove("supplier");
            }
            return supplierEntity.Id;
        }

        public IEnumerable<SupplierDTO> GetSuppliers()
        {
            if (_cache.TryGetValue("supplier", out List<SupplierDTO>? listSuplier))
            {
                return listSuplier ?? Enumerable.Empty<SupplierDTO>();
            }

            var supplierEntity = _context.Suppliers.Select(p => _mapper.Map<SupplierDTO>(p));
            _cache.Set("supplier", supplierEntity, TimeSpan.FromMinutes(30));
            return supplierEntity;
        }

        public int DelSupplier(int suplpliaerId)
        {
            var entitySupplier = _context.Suppliers.FirstOrDefault(x => x.Id.Equals(suplpliaerId));

            if (entitySupplier != null)
            {
                _context.Suppliers.Remove(entitySupplier);
                _context.SaveChanges();
                _cache.Remove("supplier");
                return entitySupplier.Id;
            }
            return -1;

        }
    }
}
