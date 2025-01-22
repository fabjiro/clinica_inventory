using Application.Dto.Response.Consult;
using Application.Helpers;
using Application.Queries.Consult;
using Application.Specifications.Consult;
using Ardalis.Result;
using AutoMapper;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Consult;

public class GetAllConsultQueryHandler : IRequestHandler<GetAllConsultQuery, Result<List<ConsultDtoRes>>>
{
    private readonly IAsyncRepository<ConsultEntity> _repository;
    private readonly IMapper _mapper;

    public GetAllConsultQueryHandler(IAsyncRepository<ConsultEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<List<ConsultDtoRes>>> Handle(GetAllConsultQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.ListAsync(new GetConsultIncludeSpecifications(
                StartDate: request.StartDate,
                EndDate: request.EndDate
            ), cancellationToken);

            return Result.Success(_mapper.Map<List<ConsultDtoRes>>(result));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }   
    }
}