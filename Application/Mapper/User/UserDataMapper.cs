using Application.Dto.Request.Rol;
using Application.Dto.Response.Image;
using Application.Dto.Response.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.User;

public class UserDataMapper : Profile
{
    public UserDataMapper()
    {
        CreateMap<UserEntity, UserBasicResDto>();

        CreateMap<RolEntity, RolResDto>();

        CreateMap<ImageEntity, ImageResDto>();
    }
}