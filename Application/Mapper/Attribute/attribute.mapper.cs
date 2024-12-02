using Application.Dto.Response.Attributes;
using AutoMapper;
using Domain.Entities.Attributes;

namespace Application.Mapper.Attribute;
public class AttributeMapper : Profile
{
    public AttributeMapper()
    {
        CreateMap<AttributesEntity, AttributesBasicResDto>();

        CreateMap<AttributesValuesEntity, AttributesBasicResDto>();
    }
}