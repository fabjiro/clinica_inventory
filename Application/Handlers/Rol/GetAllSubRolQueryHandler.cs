using Application.Dto.Response.Rol;
using Application.Helpers;
using Application.Querys.Rol;
using Application.Specifications.Rol;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Rol;

public class GetAllSubRolQueryHandler : IRequestHandler<GetAllSubRolQuery, Result<List<SubRolResDto>>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<SubRolEntity> _subRolRepository;

    public GetAllSubRolQueryHandler(IMapper mapper, IAsyncRepository<SubRolEntity> subRolRepository)
    {
        _mapper = mapper;
        _subRolRepository = subRolRepository;
    }

    public async Task<Result<List<SubRolResDto>>> Handle(GetAllSubRolQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _subRolRepository.ListAsync(new SubRolByIdIncludesSpecifications(), cancellationToken);
            return Result.Success(_mapper.Map<List<SubRolResDto>>(result));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}