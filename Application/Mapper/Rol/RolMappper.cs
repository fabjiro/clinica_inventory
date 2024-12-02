using Application.Dto.Request.Rol;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Rol;

public class RolMappper : Profile
{
    public RolMappper()
    {
        CreateMap<RolEntity, RolResDto>();
    }
}