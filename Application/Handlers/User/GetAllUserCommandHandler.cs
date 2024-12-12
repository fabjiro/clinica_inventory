using Application.Dto.Response.User;
using Application.Queries.User;
using Application.Specifications.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.User;

public class GetAllUserCommandHandler : IRequestHandler<GetAllUserQuery, Result<List<UserBasicResDto>>>
{
    private readonly IAsyncRepository<UserEntity> _repository;
    private readonly IMapper _mapper;

    public GetAllUserCommandHandler(IAsyncRepository<UserEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<List<UserBasicResDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {

        var users = await _repository.ListAsync(cancellationToken);
        return Result.Success(users.Select(_mapper.Map<UserBasicResDto>).ToList());
    }
}
