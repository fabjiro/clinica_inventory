using Application.Dto.Response.User;
using Application.Dto.User;
using Application.Helpers;
using Application.Queries.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.User;

public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, Result<List<UserBasicResDto>>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public GetAllPatientsQueryHandler(IAsyncRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<UserBasicResDto>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userRepository.ListAsync(new GetUserByRolSpecification(Guid.Parse(RolConst.Consultation)), cancellationToken);
            return Result.Success(_mapper.Map<List<UserBasicResDto>>(result));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
