using Application.Dto.Response.Exam;
using MediatR;

namespace Application.Queries.Exam;

public record GetAllExamQuery : IRequest<List<ExamDto>>;