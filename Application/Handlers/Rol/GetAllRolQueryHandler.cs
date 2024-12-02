
using Application.Dto.Request.Rol;
using Application.Queries.Rol;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.Rol;

public class GetAllRolQueryHandler : IRequestHandler<GetAllRolQuery, Result<List<RolResDto>>>
{
    private readonly IAsyncRepository<RolEntity> _rolRepository;
    private readonly IMapper _mapper;

    public GetAllRolQueryHandler(IAsyncRepository<RolEntity> repository, IMapper mapper)
    {
        _rolRepository = repository;
        _mapper = mapper;
    }

    public async Task<Result<List<RolResDto>>> Handle(GetAllRolQuery request, CancellationToken cancellationToken)
    {
        var result = await _rolRepository.ListAsync(cancellationToken);
        return Result.Success(_mapper.Map<List<RolResDto>>(result));
    }
}