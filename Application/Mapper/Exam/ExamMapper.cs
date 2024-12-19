using Application.Dto.Response.Exam;
using AutoMapper;
using Domain.Entities;

namespace Application.Commands.Exam;

public class ExamMapper : Profile
{
    public ExamMapper()
    {
        CreateMap<ExamEntity, ExamDto>();
    }
}