using AutoMapper;
using Lesson_3_GraphQL.Models;
using Lesson_3_GraphQL.Models.DTO;

namespace Lesson_3_GraphQL.Repo
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, PostProductDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Product, ProductDTO>(MemberList.Destination).ReverseMap();          
            CreateMap<Category, PostCategoryDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, PostStorageDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Storehouse, PostStorehouseDTO>(MemberList.Destination).ReverseMap();
            CreateMap<Storehouse, StorehouseDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
