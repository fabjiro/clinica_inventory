using Application.Helpers;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Queries.Consult;

public class DeleteConsultCommandHandler : IRequestHandler<DeleteConsultCommand, Result<Guid>>
{
    private readonly IAsyncRepository<ConsultEntity> _repository;
    private readonly IAsyncRepository<PatientEntity> _patientRepository;

    public DeleteConsultCommandHandler(IAsyncRepository<ConsultEntity> repository, IAsyncRepository<PatientEntity> patientRepository)
    {
        _repository = repository;
        _patientRepository = patientRepository;
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

            var patient = await _patientRepository.GetByIdAsync(consult.PatientId, cancellationToken);

            patient!.ConsultCount -= 1;
            consult.IsDeleted = true;

            consult.SetDeletedInfo(request.UserId);

            await _patientRepository.UpdateAsync(patient, cancellationToken);
            await _repository.UpdateAsync(consult, cancellationToken);

            return Result.Success(consult.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}