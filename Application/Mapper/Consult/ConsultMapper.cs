using Application.Dto.Response.Consult;
using AutoMapper;

namespace Application.Mapper.Consult;

public class ConsultMapper : Profile
{
    public ConsultMapper()
    {
        CreateMap<ConsultEntity, ConsultDtoRes>();
    }
}