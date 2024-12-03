using Application.Dto.Response.CivilStatus;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.CivilStatus;

public class CivilStatusMapper :Profile
{
    public CivilStatusMapper()
    {
        CreateMap<CivilStatusEntity, CivilStatusResDto>();
    }
}