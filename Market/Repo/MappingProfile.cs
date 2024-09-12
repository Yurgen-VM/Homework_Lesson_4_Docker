using AutoMapper;
using Market.Models;
using Market.Models.DTO;

namespace Market.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, PostProductModel>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductModel>(MemberList.Destination).ReverseMap();
            CreateMap<Product, PriceProductModel>(MemberList.Destination).ReverseMap();
            CreateMap<Product, DeleteModel>(MemberList.Destination).ReverseMap();
            CreateMap<Category, PostCategoryModel>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryModel>(MemberList.Destination).ReverseMap();
            CreateMap<Category, DeleteModel>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageModel>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, PostStorageModel>(MemberList.Destination).ReverseMap();
           
        }
    }
}
