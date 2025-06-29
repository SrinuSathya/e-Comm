using AutoMapper;
using MenuApi.Domain.Entities;
using MenuApi.DTOs;

namespace MenuApi.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItem, MenuItemDto>().ReverseMap();
            CreateMap<MenuItem, CreateOrUpdateMenuItemDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}

