using Application.Dto.Response.Report;
using Application.Helpers;
using Application.Queries.Report;
using Application.Specifications.Patient;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Report;

public class ReportRegisterPatientQueryHandler : IRequestHandler<ReportRegisterPatientQuery, Result<List<ReportRegisterPatientResDto>>>
{
    private readonly IAsyncRepository<PatientEntity> _patientRepository;
    private readonly IMapper _mapper;
    public ReportRegisterPatientQueryHandler(IAsyncRepository<PatientEntity> patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ReportRegisterPatientResDto>>> Handle(ReportRegisterPatientQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var patients = await _patientRepository.ListAsync(
                new GetPatientByRangeDateSpecifications(
                    startDate: request.StartDate,
                    endDate: request.EndDate
                ), cancellationToken
            );

            return Result.Success(_mapper.Map<List<ReportRegisterPatientResDto>>(patients));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}