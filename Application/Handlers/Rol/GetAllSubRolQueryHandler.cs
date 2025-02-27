using Application.Dto.Response.Rol;
using Application.Helpers;
using Application.Querys.Rol;
using Application.Specifications.Page;
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
    private readonly IAsyncRepository<PagePermitsEntity> _pagePermitsRepository;

    public GetAllSubRolQueryHandler(IMapper mapper, IAsyncRepository<SubRolEntity> subRolRepository, IAsyncRepository<PagePermitsEntity> pagePermitsRepository)
    {
        _mapper = mapper;
        _subRolRepository = subRolRepository;
        _pagePermitsRepository = pagePermitsRepository;
    }

    public async Task<Result<List<SubRolResDto>>> Handle(GetAllSubRolQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _subRolRepository.ListAsync(new SubRolByIdIncludesSpecifications(), cancellationToken);

            if (result is null)
            {
                return Result.Success(new List<SubRolResDto>());
            }

            var resulMapped = _mapper.Map<List<SubRolResDto>>(result);


            for(int i = 0; i < resulMapped.Count; i++)
            {
                var resultPage = await _pagePermitsRepository.ListAsync(new GetPagePermitSpecifications(
                    rolId: resulMapped[i]!.Id,
                    include: true
                ), cancellationToken);

                resulMapped[i]!.Pages = resultPage.Select(x => x.Page).ToList()!;
            }
            
            
            return Result.Success(resulMapped);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}