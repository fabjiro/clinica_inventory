using Application.Dto.Response.Report;
using Application.Helpers;
using Application.Querys.Report;
using Application.Specifications.Patient;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Report;

public class ReportMasterQueryHandler : IRequestHandler<ReportMasterQuery, Result<List<ReportMasterResDto>>>
{
    private readonly IAsyncRepository<PatientEntity> _patientRepository;
    private readonly IMapper _mapper;

    public ReportMasterQueryHandler( IAsyncRepository<PatientEntity> patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ReportMasterResDto>>> Handle(ReportMasterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var patients = await _patientRepository.ListAsync(
                new GetPatientByRangeDateSpecifications(
                    startDate: request.StartDate,
                    endDate: request.EndDate
                ), cancellationToken
            );

            var result = _mapper.Map<List<ReportMasterResDto>>(patients);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}