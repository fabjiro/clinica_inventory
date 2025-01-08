using Application.Dto.Response.Consult;
using Application.Helpers;
using Application.Queries.Consult;
using Application.Specifications.Consult;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Consult;

public class GetConsultByPatientIdQueryHandler : IRequestHandler<GetConsultByPatientIdQuery, Result<List<ConsultDtoRes>>>
{
    private readonly IAsyncRepository<ConsultEntity> _repository;

    private readonly IAsyncRepository<PatientEntity> _patientRepository;
    private readonly IMapper _mapper;


    public GetConsultByPatientIdQueryHandler(IAsyncRepository<ConsultEntity> repository, IAsyncRepository<PatientEntity> patientRepository, IMapper mapper)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ConsultDtoRes>>> Handle(GetConsultByPatientIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var patientEntity = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);

            if (patientEntity is null)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Patient not found",}
                });
            }

            var result = await _repository.ListAsync(
                new GetConsultIncludeSpecifications(
                    PatientId: request.PatientId
                ),
                cancellationToken  
            );

            return Result.Success(_mapper.Map<List<ConsultDtoRes>>(result));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
