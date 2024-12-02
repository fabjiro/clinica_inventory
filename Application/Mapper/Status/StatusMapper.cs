using Application.Dto.Response.Status;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Status;

public class StatusMapper : Profile
{
    public StatusMapper()
    {
        CreateMap<StatusEntity, StatusResDto>();
    }
}