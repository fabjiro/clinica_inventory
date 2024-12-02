using Application.Dto.Response.Category;
using Application.Dto.Response.Image;
using Application.Dto.Response.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Product;

public class ProductBasicMapper : Profile
{
    public ProductBasicMapper()
    {
        CreateMap<ProductEntity, ProductBasicResDto>();

        CreateMap<CategoryEntity, CategoryResDto>();

        CreateMap<ImageEntity, ImageResDto>();
    }
}