using Application.Dto.Response.CivilStatus;
using Application.Helpers;
using Application.Queries.CivilStatus;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.CivilStatus;

public class GetAllCivilStatusQueryHandler : IRequestHandler<GetAllCivilStatusQuery, Result<List<CivilStatusResDto>>>
{
    private readonly IAsyncRepository<CivilStatusEntity> _civilStatusRepository;
    private readonly IMapper _mapper;

    public GetAllCivilStatusQueryHandler(IAsyncRepository<CivilStatusEntity> civilStatusRepository, IMapper mapper)
    {
        _civilStatusRepository = civilStatusRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CivilStatusResDto>>> Handle(GetAllCivilStatusQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _civilStatusRepository.ListAsync(cancellationToken);
            return Result.Success(_mapper.Map<List<CivilStatusResDto>>(result));   
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
