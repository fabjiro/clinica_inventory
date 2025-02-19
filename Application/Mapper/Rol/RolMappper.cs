using Application.Dto.Request.Rol;
using Application.Dto.Response.Rol;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Rol;

public class RolMappper : Profile
{
    public RolMappper()
    {
        CreateMap<RolEntity, RolResDto>();

        CreateMap<SubRolEntity, SubRolResDto>();

        // CreateMap<SubRolEntity, RolResDto>();
    }
}