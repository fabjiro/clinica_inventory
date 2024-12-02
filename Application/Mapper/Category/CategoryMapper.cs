using Application.Dto.Response.Category;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Category;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CategoryEntity, CategoryResDto>()
            .ForMember(dest => dest.CountProducts, opt => opt.MapFrom(src => src.Products.Count));
    }
}