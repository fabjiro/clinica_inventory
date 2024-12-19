using Application.Dto.Response.Group;
using Application.Queries.Group;
using Application.Specifications.Exam;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Group;

public class GetAllGroupQueryHandler : IRequestHandler<GetAllGroupQuery, List<GroupDto>>
{
    private readonly IAsyncRepository<GroupEntity> _groupRepository;
    private readonly IMapper _mapper;

    public GetAllGroupQueryHandler(IAsyncRepository<GroupEntity> groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<List<GroupDto>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.ListAsync(cancellationToken);
        return _mapper.Map<List<GroupDto>>(groups);
    }
}