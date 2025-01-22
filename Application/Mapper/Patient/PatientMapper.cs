using Application.Dto.Response.Patient;
using Application.Dto.Response.Report;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Patient;

public class PatientMapper : Profile
{
    public PatientMapper()
    {
        CreateMap<PatientEntity, PatientResDto>();

        CreateMap<PatientEntity, ReportRegisterPatientResDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")));
    }
}