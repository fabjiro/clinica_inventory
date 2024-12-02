using Application.Dto.Response.Shop;
using AutoMapper;
using Domain.Entities.Shop;

namespace Application.Mapper.Shop;

public class ShopMapper : Profile
{
    public ShopMapper()
    {
        CreateMap<ShopEntity, ShopResponseDto>();

        CreateMap<ShopTypeEntity, ShopTypeResponseDto>();
    }
}