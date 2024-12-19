using Application.Dto.Response.Group;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Group;

public class GroupMapper : Profile
{
    public GroupMapper()
    {
        CreateMap<GroupEntity, GroupDto>();
    }
}