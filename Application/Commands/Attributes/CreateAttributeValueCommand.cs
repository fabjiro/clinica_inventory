using Application.Dto.Response.Attributes;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Attributes;

public record CreateAttributeValueCommand(string UserId, Guid AttributeId, string Value) : IRequest<Result<AttributesBasicResDto>>;