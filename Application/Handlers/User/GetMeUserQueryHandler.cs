using Application.Dto.Response.User;
using Application.Queries.User;
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
    private readonly IMapper _mapper;

    public GetMeUserQueryHandler(IAsyncRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
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

        return Result<UserBasicResDto>.Success(_mapper.Map<UserBasicResDto>(user));
    }
}
