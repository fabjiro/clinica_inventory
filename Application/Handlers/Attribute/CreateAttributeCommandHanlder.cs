using Application.Commands.Attributes;
using Application.Dto.Response.Attributes;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities.Attributes;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Attribute;

public class CreateAttributeCommandHanlder : IRequestHandler<CreateAttributeCommand, Result<AttributesBasicResDto>>
{
    private readonly IAsyncRepository<AttributesEntity> _repository;
    private readonly IMapper _mapper;
    public CreateAttributeCommandHanlder(IAsyncRepository<AttributesEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Result<AttributesBasicResDto>> Handle(CreateAttributeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var attribute = new AttributesEntity(request.Name);
            attribute.SetCreationInfo(request.UserId);

            await _repository.AddAsync(attribute, cancellationToken);
            return Result.Success(_mapper.Map<AttributesBasicResDto>(attribute));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}