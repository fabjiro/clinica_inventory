using Application.Commands.Exam;
using Application.Helpers;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Exam;

public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, Result<Guid>>
{
    private readonly IAsyncRepository<ExamEntity> _examRepository;

    public DeleteExamCommandHandler(IAsyncRepository<ExamEntity> examRepository)
    {
        _examRepository = examRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
    {
       try
       {
            var exam = await _examRepository.GetByIdAsync(request.Id, cancellationToken);
            if (exam == null)
            {
                return Result<Guid>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Exam not found",}
            });
            }

            exam.IsDeleted = true;
            await _examRepository.UpdateAsync(exam, cancellationToken);

            return Result<Guid>.Success(exam.Id);
       }
       catch (Exception ex)
       {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
       }
    }
}