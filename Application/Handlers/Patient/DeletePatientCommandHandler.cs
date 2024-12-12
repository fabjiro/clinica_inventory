using Application.Commands.Patient;
using Application.Dto.Response.Patient;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Patient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Result<PatientResDto>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<PatientEntity> _patientRepository;
    public DeletePatientCommandHandler(IMapper mapper, IAsyncRepository<PatientEntity> patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
    }
    public async Task<Result<PatientResDto>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);
            if (patient is null)
            {
                return Result<PatientResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Patient not found",}
                });
            }

            patient.SetDeletedInfo(request.UserId);
            await _patientRepository.UpdateAsync(patient, cancellationToken);

            return Result.Success(_mapper.Map<PatientResDto>(patient));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
