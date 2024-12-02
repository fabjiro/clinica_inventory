using Application.Dto.Response.Attributes;
using Application.Queries.Attributes;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities.Attributes;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Attribute;

public class GetAllAttributeQueryHandler : IRequestHandler<GetAllAttributesQuery, Result<List<AttributesBasicResDto>>>
{
    private readonly IAsyncRepository<AttributesEntity> _repository;
    private readonly IMapper _mapper;

    public GetAllAttributeQueryHandler(IAsyncRepository<AttributesEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<List<AttributesBasicResDto>>> Handle(GetAllAttributesQuery request, CancellationToken cancellationToken)
    {
        var attributes = await _repository.ListAsync(cancellationToken);
        return Result.Success(attributes.Select(_mapper.Map<AttributesBasicResDto>).ToList());

    }
}