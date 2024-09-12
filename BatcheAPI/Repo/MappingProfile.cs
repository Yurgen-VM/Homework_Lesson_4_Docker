using AutoMapper;
using BatcheAPI.DB;
using BatcheAPI.DB.DTO;

namespace BatcheAPI.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Batch, BatchDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Batch, PostBatchDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Product, PostProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Supplier, SupplierDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Supplier, PostSupplierDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
