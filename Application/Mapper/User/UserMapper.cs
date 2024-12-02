using Application.Dto.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, AuthUserResDto>();
    }
}