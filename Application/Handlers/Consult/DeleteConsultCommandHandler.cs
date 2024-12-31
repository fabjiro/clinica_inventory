using Application.Helpers;
using Ardalis.Result;
using Domain.Interface;
using MediatR;

namespace Application.Queries.Consult;

public class DeleteConsultCommandHandler : IRequestHandler<DeleteConsultCommand, Result<Guid>>
{
    private readonly IAsyncRepository<ConsultEntity> _repository;

    public DeleteConsultCommandHandler(IAsyncRepository<ConsultEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(DeleteConsultCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var consult = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (consult == null)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Consult not found",}
                });
            }

            consult.IsDeleted = true;
            consult.SetDeletedInfo(request.UserId);

            await _repository.UpdateAsync(consult, cancellationToken);

            return Result.Success(consult.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}