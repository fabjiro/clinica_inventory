using Application.Dto.Response.Attributes;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Attributes;

public record CreateAttributeCommand(string UserId, string Name) : IRequest<Result<AttributesBasicResDto>>;