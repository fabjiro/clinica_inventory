using Application.Dto.Response.Exam;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Exam;

public record UpdateExamCommand(Guid Id, string? Name = null, Guid? Group = null) : IRequest<Result<ExamDto>> { };