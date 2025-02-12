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

        CreateMap<PatientEntity, ReportRegisterPatientResDto>();
        
        CreateMap<PatientEntity, ReportMasterResDto>()
            .ForMember(dest => dest.NamePatient, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PhonePatient, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactPerson))
            .ForMember(dest => dest.CountConsult, opt => opt.MapFrom(src => src.ConsultCount));
    }
}