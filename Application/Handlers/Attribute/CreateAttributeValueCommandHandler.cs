using Application.Commands.Attributes;
using Application.Dto.Response.Attributes;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities.Attributes;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Attribute;

public class CreateAttributeValueCommandHandler : IRequestHandler<CreateAttributeValueCommand, Result<AttributesBasicResDto>>
{
    private readonly IAsyncRepository<AttributesValuesEntity> _repository;
    private readonly IAsyncRepository<AttributesEntity> _attributeRepository;
    private readonly IMapper _mapper;

    public CreateAttributeValueCommandHandler(IAsyncRepository<AttributesValuesEntity> repository, IMapper mapper, IAsyncRepository<AttributesEntity> attributeRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _attributeRepository = attributeRepository;
    }

    public async Task<Result<AttributesBasicResDto>> Handle(CreateAttributeValueCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var attribute = await _attributeRepository.GetByIdAsync(request.AttributeId, cancellationToken);

            if (attribute is null)
            {
                return Result<AttributesBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Attribute not found",}
                });
            }

            var attributeValue = new AttributesValuesEntity(request.AttributeId, request.Value);
            attributeValue.SetCreationInfo(request.UserId);

            await _repository.AddAsync(attributeValue, cancellationToken);
            return Result.Success(_mapper.Map<AttributesBasicResDto>(attributeValue));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}