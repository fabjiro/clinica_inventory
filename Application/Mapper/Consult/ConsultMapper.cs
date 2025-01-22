using Application.Dto.Response.Consult;
using Application.Dto.Response.Report;
using AutoMapper;

namespace Application.Mapper.Consult;

public class ConsultMapper : Profile
{
    public ConsultMapper()
    {
        CreateMap<ConsultEntity, ConsultDtoRes>();

        CreateMap<ConsultEntity, ReportRegistrationByUserResDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient!.Name))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserCreatedBy!.Name))
            .ForMember(dest => dest.Diagnostic, opt => opt.MapFrom(src => src.Diagnosis));

        
        CreateMap<ConsultEntity, DiagnosticsResDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Patient!.Name));
    }
}