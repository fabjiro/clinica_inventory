using Application.Dto.Response.User;
using Application.Queries.User;
using Application.Specifications.Page;
using Application.Specifications.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.User;

public class GetMeUserQueryHandler : IRequestHandler<GetMeUserQuery, Result<UserBasicResDto>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IAsyncRepository<PagePermitsEntity> _pagePermitsRepository;
    private readonly IMapper _mapper;

    public GetMeUserQueryHandler(IAsyncRepository<UserEntity> userRepository, IMapper mapper, IAsyncRepository<PagePermitsEntity> pagePermitsRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _pagePermitsRepository = pagePermitsRepository;
    }

    public async Task<Result<UserBasicResDto>> Handle(GetMeUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByIdIncludesSpecifications(request.Id), cancellationToken);


        if (user is null)
        {
            return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "User not found",}
            });
        }

        var  pagePermitsa  = await _pagePermitsRepository.ListAsync(new GetPagePermitSpecifications(
            rolId: user.SubRolId,
            include: true   
        ), cancellationToken);

        var mapper = _mapper.Map<UserBasicResDto>(user);

        mapper.Routes = [.. pagePermitsa.Select(x => x.Page?.Url)];

        return Result<UserBasicResDto>.Success(mapper);
    }
}
