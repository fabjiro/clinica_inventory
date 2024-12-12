using Application.Dto.Response.Patient;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Patient;

public class PatientMapper : Profile
{
    public PatientMapper()
    {
        CreateMap<PatientEntity, PatientResDto>();
    }
}