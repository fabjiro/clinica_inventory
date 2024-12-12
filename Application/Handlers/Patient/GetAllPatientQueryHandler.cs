using Application.Dto.Response.Patient;
using Application.Helpers;
using Application.Queries.Patient;
using Application.Specifications.Patient;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Patient;

public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, Result<List<PatientResDto>>>
{
    private readonly IAsyncRepository<PatientEntity> _patientRepository;
    private readonly IMapper _mapper;

    public GetAllPatientQueryHandler(IAsyncRepository<PatientEntity> patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<PatientResDto>>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var patiens = await _patientRepository.ListAsync(new InclusePatientSpecification(),cancellationToken);
            return Result.Success(_mapper.Map<List<PatientResDto>>(patiens));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
