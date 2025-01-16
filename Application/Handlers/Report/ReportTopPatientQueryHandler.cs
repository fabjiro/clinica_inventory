using Application.Dto.Response.Report;
using Application.Helpers;
using Application.Queries.Report;
using Application.Specifications.Patient;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Report;

public class ReportTopPatientQueryHandler : IRequestHandler<ReportTopPatientQuery, Result<List<ReportTopPatientDto>>>
{
    private readonly IAsyncRepository<PatientEntity> _patientRepository;

    public ReportTopPatientQueryHandler(IAsyncRepository<PatientEntity> patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<List<ReportTopPatientDto>>> Handle(ReportTopPatientQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _patientRepository.ListAsync(new GetTopPatientSpecifications(
                take: request.Top
            ), cancellationToken);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}