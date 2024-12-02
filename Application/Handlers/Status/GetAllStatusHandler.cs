using Application.Dto.Response.Status;
using Application.Queries.Status;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers;


public class GetAllStatusHandler : IRequestHandler<GetAllStatusQuery, Result<List<StatusResDto>>>
{
    private readonly IAsyncRepository<StatusEntity> _statusRepository;
    private readonly IMapper _mapper;

    public GetAllStatusHandler(IAsyncRepository<StatusEntity> statusRepository, IMapper mapper)
    {
        _statusRepository = statusRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<StatusResDto>>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
    {
       var result = await _statusRepository.ListAsync(cancellationToken);
        return Result.Success(_mapper.Map<List<StatusResDto>>(result));
    }
}