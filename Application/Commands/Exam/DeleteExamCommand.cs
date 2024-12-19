using Ardalis.Result;
using MediatR;

namespace Application.Commands.Exam;

public record DeleteExamCommand(Guid Id) : IRequest<Result<Guid>>;