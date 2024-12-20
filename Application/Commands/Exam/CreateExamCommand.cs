using Application.Dto.Response.Exam;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Exam;

public record CreateExamCommand(string UserId, string Name, Guid GroupId) : IRequest<Result<ExamDto>> { }